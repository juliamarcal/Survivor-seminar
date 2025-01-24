from sqlalchemy import Column, Integer, String, Text
from model.base import Base

class Tip(Base):
    __tablename__ = 'Tips'
    
    id = Column(Integer, primary_key=True, autoincrement=True)
    title = Column(String(255), nullable=False)
    info = Column(Text, nullable=False)

    def __repr__(self):
        return f"<Tip {self.title}>"
