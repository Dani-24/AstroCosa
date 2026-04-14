import strawberry
from .types import Game, GameInput, Score, ScoreInput
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
    def register_score(self, data: ScoreInput) -> Score:
        
        game_ref = db.collection("Game").document(data.game_id)
        score_ref = game_ref.collection("Score").document()

        score_data = {
            "player_id": data.player_id,
            "points": data.points,
            "kills": data.kills
        }

        score_ref.set(score_data)

        return Score(id=score_ref.id, **score_data)

    @strawberry.mutation
    def end_game(self, game_id: str) -> Game:
        
        # TODO: Change state -> Ended. Then return the Game or a custom ErrorGameNotFound
        game_ref = db.collection("Game").document(game_id)
        doc = game_ref.get()

        game_ref.update({
            "state": "Finished"
        })

        game_data = doc.to_dict()
        game_data["state"] = "Finished"

        return Game(id=game_id, **game_data)