from fastapi import FastAPI, Depends
from fastapi.middleware.cors import CORSMiddleware
import strawberry
import uvicorn 
from strawberry.fastapi import GraphQLRouter, BaseContext
from schema import Query, Mutation
from auth import get_actual_user
from typing import Optional, Dict, Any
from fastapi.staticfiles import StaticFiles

app = FastAPI(title="PinguAPI")

schema = strawberry.Schema(query=Query, mutation=Mutation)

# class ContextGraphQL(BaseContext):
#     def __init__(self, user: Optional[Dict[str, Any]]):
#         self.user = user

# def generate_context(user: Optional[Dict[str, Any]] = Depends(get_actual_user)):
#     return ContextGraphQL(user=user)

async def get_context(user = Depends(get_actual_user)):
    return {
        "user": user
    }

graphql_app = GraphQLRouter(
    schema,
    context_getter=get_context
)

origins = [
    "http://localhost:5500",
    "http://127.0.0.1:5500",
    "http://localhost:8081",
]

app.add_middleware(
    CORSMiddleware,
    allow_origins=origins,
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)


app.include_router(graphql_app, prefix="/graphql")

app.mount("/unity", StaticFiles(directory="static/unity"), name="unity")

@app.get("/")
def root():
    return {"msg": "PinguAPI is working succesfully"}

if __name__ == "__main__":
    uvicorn.run("main:app", host="0.0.0.0", port=8080, reload=True)