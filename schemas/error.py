from pydantic import BaseModel

class ErrorSchema(BaseModel):
    """ message to be displayed
    """
    message: str