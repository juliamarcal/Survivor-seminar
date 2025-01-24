from sqlalchemy import Column, Integer, String, Date, Text, DateTime, func

from model.base import Base

class Customer(Base):
    __tablename__ = 'Customer'
    
    UniqueID = Column(Integer, primary_key=True)
    name = Column(String(100), nullable=False)
    email = Column(String(100), unique=True, nullable=False)
    surname = Column(String(100), nullable=False)
    birth_date = Column(DateTime, nullable=False)
    gender = Column(String(10), nullable=False)
    description = Column(Text, nullable=True)
    astrological_sign = Column(String(50), nullable=True)
    phone_number = Column(String(15), nullable=False, unique=True)
    address = Column(String(200), nullable=True)
    joined_on = Column(DateTime, default=func.now())

    def __init__(self, name, email, surname, birth_date, gender, phone_number, 
                 description=None, astrological_sign=None, address=None):
        self.name = name
        self.email = email
        self.surname = surname
        self.birth_date = birth_date
        self.gender = gender
        self.description = description
        self.astrological_sign = astrological_sign
        self.phone_number = phone_number
        self.address = address
    
    def __repr__(self):
        return f'<Customer {self.name} {self.surname}>'