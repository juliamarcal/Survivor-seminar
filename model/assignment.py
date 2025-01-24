from sqlalchemy import Column, Integer

from model.base import Base

class Assignment(Base):
    __tablename__ = 'Assignment relation'

    customer_id = Column(Integer, primary_key=True)
    employee_id = Column(Integer, nullable=True)

    def __init__(self, cust_id, empl_id):
        self.customer_id = cust_id
        self.employee_id = empl_id
