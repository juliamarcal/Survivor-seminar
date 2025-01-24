from sqlalchemy import create_engine, Column, Integer, String, Date, Float, ForeignKey
from model.base import Base
import json
from sqlalchemy.orm import sessionmaker

class SignCompatibility(Base):
    __tablename__ = 'SignCompatibility'

    id = Column(Integer, primary_key=True, autoincrement=True)
    sign1 = Column(String(20), nullable=False)
    sign2 = Column(String(20), nullable=False)
    compatibility_score = Column(Integer, nullable=False)
    description = Column(String(100), nullable=False)

    def __init__(self, sign1, sign2, compatibility_score, description):
        self.sign1 = sign1
        self.sign2 = sign2
        self.compatibility_score = compatibility_score
        self.description = description

    def __repr__(self):
        return f'<AstrologicalCompatibility {self.sign1} - {self.sign2}: {self.compatibility_score}>'



def insert_data_from_json(engine, session, json_file):
    with open(json_file, 'r') as file:
        data = json.load(file)

    for entry in data:
        record = SignCompatibility(**entry)
        session.add(record)

    session.commit()
    session.close()
