from sqlalchemy import Column, Integer, String, DateTime, Float, ForeignKey
from model.base import Base

class Event(Base):
    __tablename__ = 'Event'
    
    UniqueID = Column(Integer, primary_key=True)
    name = Column(String(100), nullable=False)
    date = Column(DateTime, nullable=False)
    max_participants = Column(Integer, nullable=False)
    location_x = Column(Float, nullable=True)
    location_y = Column(Float, nullable=True)
    type = Column(String(50), nullable=False)
    employee_id = Column(Integer, ForeignKey('Employee.UniqueID'), nullable=False)
    location_name = Column(String(100), nullable=True)

    def __init__(self, name, date, max_participants, type, employee_id, 
                 location_x=None, location_y=None, location_name=None):
        self.name = name
        self.date = date
        self.max_participants = max_participants
        self.location_x = location_x
        self.location_y = location_y
        self.type = type
        self.employee_id = employee_id
        self.location_name = location_name

    def __repr__(self):
        return f'<Event {self.name} on {self.date}>'
