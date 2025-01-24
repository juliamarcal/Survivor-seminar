from pydantic import BaseModel
from typing import Optional

class TipSchema(BaseModel):
    id: int
    title: str
    info: str

    class Config:
        orm_mode = True

class TipUpdateSchema(BaseModel):
    id: Optional[int]
    title: Optional[str]
    info: Optional[str]

    class Config:
        orm_mode = True
