import strawberry
from firebase_conf import db
from typing import List, Optional
from .types import Game, GameInput, Score, ScorePlayer

@strawberry.type
class GamesQuery:
   
    @strawberry.field
    def get_games(
        self,
        map: Optional[str] = None,
        state: Optional[str] = None,
        limit: int = 5,
        offset: int = 0
        ) -> List[Game]:

        query = db.collection("Game")

        if map:
            query = query.where("map", "==", map)

        if state:
            query = query.where("state", "==", state)

        if offset > 0:
            query = query.offset(offset)

        docs = query.limit(limit).stream()

        return [Game(id=doc.id, **doc.to_dict()) for doc in docs]
    
    @strawberry.field
    def get_scores(
    self, 
    game_id: str,
    ) -> List[ScorePlayer]:
        
        scores_ref = db.collection("Game").document(game_id).collection("Score")
        scores_docs = scores_ref.stream()

        results = []

        for doc in scores_docs:
            score_data = doc.to_dict()

            player_id = score_data["player_id"]

            player_doc = db.collection("Player").document(player_id).get()
            player_data = player_doc.to_dict() if player_doc.exists else {}

            results.append(
                ScorePlayer(
                    id=doc.id,
                    player_id=player_id,
                    player_name=player_data.get("nickname", "Unknown"),
                    points=score_data.get("points"),
                    kills=score_data.get("kills"),
                )
            )

        return results