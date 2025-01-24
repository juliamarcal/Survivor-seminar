from pydantic import BaseModel
from typing import Optional
from datetime import date

class EncounterSchema(BaseModel):
    customer_id: int
    date: date
    rating: Optional[int]
    comment: Optional[str]
    source: Optional[str]

    class Config:
        orm_mode = True

class EncounterUpdateSchema(BaseModel):
    customer_id: Optional[int]
    date: Optional[date]
    rating: Optional[int]
    comment: Optional[str]
    source: Optional[str]

    class Config:
        orm_mode = True
