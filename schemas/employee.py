from pydantic import BaseModel
from typing import Optional
from datetime import date

class EmployeeSchema(BaseModel):
    name: str
    email: str
    surname: str
    birth_date: date
    gender: str
    work: str

    class Config:
        orm_mode = True

class EmployeeUpdateSchema(BaseModel):
    name: Optional[str]
    email: Optional[str]
    surname: Optional[str]
    birth_date: Optional[date]
    gender: Optional[str]
    work: Optional[str]

    class Config:
        orm_mode = True
