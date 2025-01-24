from sqlalchemy import Column, Integer, Date, String, Text, ForeignKey
from model.base import Base

class Encounter(Base):
    __tablename__ = 'Encounter'
    
    UniqueID = Column(Integer, primary_key=True)
    customer_id = Column(Integer, ForeignKey('Customer.UniqueID'), nullable=True)
    date = Column(Date, nullable=False)
    rating = Column(Integer, nullable=False)
    comment = Column(Text, nullable=True)
    source = Column(String(100), nullable=True)

    def __init__(self, customer_id, date, rating, comment=None, source=None):
        self.customer_id = customer_id
        self.date = date
        self.rating = rating
        self.comment = comment
        self.source = source

    def __repr__(self):
        return f'<Encounter {self.UniqueID} with customer {self.customer_id}>'
