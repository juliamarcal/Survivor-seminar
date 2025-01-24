from sqlalchemy import Column, Integer, String, DateTime
from model.base import Base

class Employee(Base):
    __tablename__ = 'Employee'
    
    UniqueID = Column(Integer, primary_key=True)
    name = Column(String(100), nullable=False)
    email = Column(String(100), unique=True, nullable=False)
    surname = Column(String(100), nullable=False)
    birth_date = Column(DateTime, nullable=False)
    gender = Column(String(10), nullable=False)
    work = Column(String(200), nullable=True)

    # __init__ constructor for the model
    def __init__(self, name, email, surname, birth_date, gender, work=None):
        self.name = name
        self.email = email
        self.surname = surname
        self.birth_date = birth_date
        self.gender = gender
        self.work = work

    def __repr__(self):
        return f'<Employee {self.name} {self.surname}>'