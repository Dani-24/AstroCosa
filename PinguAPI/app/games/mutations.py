import strawberry
from .types import Game, GameInput, Score
from datetime import datetime, timezone
from firebase_conf import db 

@strawberry.type
class GamesMutations:
    
    @strawberry.mutation
    def create_game(self, data: GameInput) -> Game:

        game_data = {
            "map": data.map,
            "state": "Playing",
            "created_on": datetime.now(timezone.utc)
        }

        created_on, ref = db.collection("Game").add(game_data)

        return Game(id=ref.id, **game_data)
    

    @strawberry.mutation
    def register_score(self) -> Score:
        # TODO: Save a ScoreInput on a Game
        pass

    @strawberry.mutation
    def end_game(self) -> Game:
        # TODO: Change state -> Ended. Then return the Game or a custom ErrorGameNotFound
        pass