from pydantic import BaseModel
from typing import Optional

class AssignmentSchema(BaseModel):
    cust_id: int
    empl_id: int

    class Config:
        orm_mode = True

class TipUpdateSchema(BaseModel):
    cust_id: Optional[int]
    empl_id: Optional[int]

    class Config:
        orm_mode = True
