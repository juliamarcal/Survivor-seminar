from os import urandom
from flask_openapi3 import Info, OpenAPI
from flask import jsonify, request, redirect, render_template, session
from flask_cors import CORS
from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker, scoped_session

from functools import wraps

from requests import post
from datetime import datetime
from base64 import b64encode

from model.sign import SignCompatibility
from model.customer import Customer
from model.employee import Employee
from model.encounter import Encounter
from model.event import Event
from model.assignment import Assignment
from model.credentials import Credentials

from metrics.customersPerPeriod import generate_graph1, get_customer_stats
from metrics.eventPerPeriod import generate_graph2, generate_metrics_for_events
from flask import send_file

info = Info(title="Soul Connection", version="2.0.0")
app = OpenAPI(__name__, info=info)
CORS(app)
app.config['SECRET_KEY'] = urandom(12)
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///database/db.sqlite3'
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = False

engine = create_engine(app.config['SQLALCHEMY_DATABASE_URI'], echo=True)
session_factory = sessionmaker(bind=engine)
Session = scoped_session(session_factory)

def login_required(f):
    @wraps(f)
    def wrap(*args, **kwargs):
        if 'access_token' in session:
            return f(*args, **kwargs)
        else:
            return redirect('/login')
    return wrap

# API METHODS

@app.get('/api/customers', summary="Get all clients")
def get_customers():
    db = Session()
    customers = db.query(Customer).all()
    db.close()
    customer_list = []

    for customer in customers:
        customer_list.append({
            'id': customer.UniqueID,
            'name': f"{customer.name} {customer.surname}",
            'email': customer.email,
            'phone': customer.phone_number,
            'profile_picture': "https://via.placeholder.com/50",  # Placeholder for now
            'payment_method_img': "https://via.placeholder.com/20"  # Placeholder for now
        })

    return jsonify({"customers": customer_list})

@app.get('/api/coaches', summary="Get all coaches")
def get_coaches():
    db = Session()
    coachs = db.query(Employee).all()
    coach_list = []

    for coach in coachs:
        num_cli = db.query(Assignment).filter_by(employee_id = coach.UniqueID).count()
        coach_list.append({
            'id': coach.UniqueID,
            'name': f"{coach.name} {coach.surname}",
            'email': coach.email,
            'number_of_clients': num_cli,
            'profile_picture': "https://via.placeholder.com/50"
        })

    db.close()
    return jsonify({"coaches": coach_list})

@app.post('/check_compatibility', summary="checks the compatibility")
def check_compatibility():
    data = request.get_json()
    customer1_id = data.get('customer1_id')
    customer2_id = data.get('customer2_id')

    if not customer1_id or not customer2_id:
        return jsonify({'error': 'Customer IDs are required'}), 400

    db = Session()
    # Retrieve customer signs from the database
    customer1 = db.query(Customer).get(customer1_id)
    customer2 = db.query(Customer).get(customer2_id)

    if not customer1 or not customer2:
        return jsonify({'error': 'One or both customers not found'}), 404

    sign1 = customer1.astrological_sign  # Assuming there's a 'sign' field in the Customer table
    sign2 = customer2.astrological_sign

    # Retrieve compatibility information from SignCompatibility table
    compatibility_record = db.query(SignCompatibility).filter_by(sign1=sign1, sign2=sign2).first()

    db.close()

    if compatibility_record:
        compatibility = compatibility_record.compatibility_score
        description = compatibility_record.description
    else:
        compatibility = 'Unknown'
        description = 'Compatibility information not found.'

    # Return the result to the frontend
    return jsonify({'compatibility': f'{compatibility} - {description}', 'customer1_name':customer1.name, 'sign1':sign1, 'customer2_name':customer2.name, 'sign2':sign2}), 200

@app.post('/api/assign', summary="assigns a customer to a coach")
def assign_customer():
    data = request.get_json()
    customer_id = data.get('customer_id')
    employee_id = data.get('employee_id')

    db = Session()
    relation = db.query(Assignment).filter(Assignment.customer_id == customer_id).first()
    if relation is not None:
        return 409
    
    relation = Assignment(customer_id, employee_id)
    db.add(relation)
    db.commit()
    db.close()
    return 200

@app.post('/login/register', summary="register an employee")
def make_register():
    data = request.get_json()
    name = data.get('name')
    password = data.get('password')
    surname = data.get('surname')
    email = data.get('email')
    gender = data.get('gender')
    birth_date = data.get('birthDate')
    work = data.get('workTitle')

    db = Session()
    duplicate = db.query(Employee).filter(Employee.email == email).first()
    if duplicate is not None:
        db.close()
        return jsonify({'error': 'email already registered'}), 409
    
    date_object = datetime.strptime(birth_date, '%Y-%m-%d').date()
    dt = datetime.combine(date_object, datetime.min.time())
    new_employee = Employee(name, email, surname, dt, gender, work)
    new_credential = Credentials(email)
    new_credential.set_password(password)

    db.add(new_employee)
    db.add(new_credential)
    db.commit()
    db.close()
    
    return jsonify({'operation': 'successiful'}), 200


@app.post('/login/auth', summary="login authorization process")
def make_login():
    json_data = request.get_json()
    email = json_data.get('email')
    password = json_data.get('password')

    db = Session()
    employee = db.query(Employee).filter(Employee.email == email).first()
    if employee is None:
        db.close()
        return jsonify({'operation': 'Email not registered'}), 401

    creds = db.query(Credentials).filter(Credentials.email == email).first()
    db.close()
    if creds is not None and creds.check_password(password):
        session['access_token'] = b64encode(bytes(str({"alg":"HS256", "typ":"JWT", 'id': employee.UniqueID, 'email': employee.email, 'name': employee.name, 'surname': employee.surname}), 'utf-8'))
        return jsonify({'operation': 'successiful'}), 200

    response = post("https://soul-connection.fr/api/employees/login", json=json_data, headers={"X-Group-Authorization": "84f672bd1ba67d839ad92fd53a87200f"})
    if response.ok:
        session['access_token'] = response.json()['access_token']
        return jsonify({'operation': 'successiful'}), 200

    return jsonify({'operation': 'Invalid Email and Password combination.'}), 401
    

@app.route('/api/get-customers-graph', methods=['GET'])
def get_customers_graph():
    db = Session()
    period = request.args.get('period', '1M')
    img = generate_graph1(period, db)
    db.close()
    return send_file(img, mimetype='image/png')

@app.route('/api/get-customer-stats', methods=['GET'])
def get_customer_stats_api():
    db = Session()
    stats = get_customer_stats(db)
    db.close()
    return jsonify(stats)

@app.route('/api/get-events-graph', methods=['GET'])
def get_events_graph():
    db = Session()
    img = generate_graph2(db)
    db.close()
    return send_file(img, mimetype='image/png')

@app.route('/api/get-events-metrics', methods=['GET'])
def get_events_metrics():
    db = Session()
    metrics = generate_metrics_for_events(db)
    db.close()

    return jsonify(metrics)

# ROUTES

@app.route('/')
@login_required
def home():
    return redirect('/dashboard')

@app.route('/dashboard')
@login_required
def dashboard():
    error = None
    return render_template('dashboard.html', error=error)




@app.route('/login')
def login():
    error = None
    return render_template('login.html', error=error)

@app.route('/register')
def register():
    error = None
    return render_template('register.html', error=error)

@app.route('/advices')
@login_required
def advices():
    error = None
    return render_template('advice.html', error=error)

@app.route('/coaches')
@login_required
def coaches():
    error = None
    return render_template('coachesList.html', error=error)

@app.route('/clients')
@login_required
def clients():
    error = None
    return render_template('costumersList.html', error=error)

@app.route('/wardrobe')
@login_required
def wardrobe():
    error = None
    return render_template('wardrobe.html', error=error)

@app.route('/events')
@login_required
def events():
    error = None
    return render_template('events.html', error=error)

@app.route('/blog')
@login_required
def blog():
    error = None
    return render_template('blog.html', error=error)

@app.route('/client')
@login_required
def client():
    error = None
    id = request.args.get('id', int)
    if id is None:
        error = "No matching id for customer"
        return redirect('/clients')
    db = Session()
    client = db.query(Customer).filter(Customer.UniqueID == id).first()
    if client is None:
        error = "No matching id for customer"
        return redirect('/clients')
    return render_template('client.html', error=error, client=client)

if __name__ == "__main__":
    app.run(debug=True)