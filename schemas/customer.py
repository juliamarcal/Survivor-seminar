from pydantic import BaseModel, EmailStr
from typing import Optional
from datetime import date

class CustomerSchema(BaseModel):
    name: str
    email: EmailStr
    surname: str
    birth_date: date
    gender: str
    description: Optional[str]
    astrological_sign: Optional[str]
    phone_number: Optional[str]
    address: Optional[str]
    joined_on: Optional[date]

    class Config:
        orm_mode = True

class CustomerUpdateSchema(BaseModel):
    name: Optional[str]
    email: Optional[EmailStr]
    surname: Optional[str]
    birth_date: Optional[date]
    gender: Optional[str]
    description: Optional[str]
    astrological_sign: Optional[str]
    phone_number: Optional[str]
    address: Optional[str]
    joined_on: Optional[date]

    class Config:
        orm_mode = True