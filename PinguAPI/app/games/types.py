import strawberry
from typing import List, Optional
from datetime import datetime

@strawberry.type
class Score:
    id: str
    player_id: str
    points: str
    kills: str

@strawberry.type
class ScorePlayer:
    id: str
    player_id: str
    player_name: str
    points: str
    kills: str

@strawberry.type
class Game:
    id: str
    map: str
    state: str
    created_on: datetime
    scores: Optional[List[Score]] = None

@strawberry.input
class GameInput:
    map: str

@strawberry.input
class ScoreInput:
    game_id: str
    player_id: str
    points: str
    kills: str