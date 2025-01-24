from sqlalchemy import Column, String
from werkzeug.security import generate_password_hash, check_password_hash

from model.base import Base

class Credentials(Base):
    __tablename__ = 'Credentials'

    email = Column(String(255), primary_key=True)
    _password_hash = Column('password', String(255), nullable=False)

    def set_password(self, password):
        """Encrypt and store the password."""
        self._password_hash = generate_password_hash(password)
    
    def check_password(self, password):
        """Check if the provided password matches the stored hash."""
        return check_password_hash(self._password_hash, password)

    def __init__(self, email):
        self.email = email

    def __repr__(self):
        return f"<Credentials(email='{self.email}')>"