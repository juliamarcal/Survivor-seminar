from sqlalchemy_utils import database_exists, create_database
from sqlalchemy.orm import sessionmaker
from sqlalchemy import create_engine

import os

from model.sign import insert_data_from_json
from model.base import Base
from model.sign import SignCompatibility
from model.customer import Customer
from model.employee import Employee
from model.encounter import Encounter
from model.event import Event
from model.tip import Tip
from model.assignment import Assignment
from model.credentials import Credentials

db_path = "database/"
if not os.path.exists(db_path):
   os.makedirs(db_path)

db_url = f'sqlite:///{db_path}db.sqlite3'
engine = create_engine(db_url, echo=False)

if not database_exists(engine.url):
    create_database(engine.url)

Base.metadata.create_all(engine)

Session = sessionmaker(bind=engine)
session = Session()

json_file_path = 'model/sign_compatibility.json'
insert_data_from_json(engine, session, json_file_path)