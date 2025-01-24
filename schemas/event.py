from pydantic import BaseModel
from typing import Optional
from datetime import date

class EventSchema(BaseModel):
    name: str
    date: date
    max_participants: int
    location_x: float
    location_y: float
    type: str
    employee_id: int
    location_name: Optional[str]

    class Config:
        orm_mode = True

class EventUpdateSchema(BaseModel):
    name: Optional[str]
    date: Optional[date]
    max_participants: Optional[int]
    location_x: Optional[float]
    location_y: Optional[float]
    type: Optional[str]
    employee_id: Optional[int]
    location_name: Optional[str]

    class Config:
        orm_mode = True
