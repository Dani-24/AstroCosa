import strawberry
from firebase_conf import db
from typing import List, Optional
from .types import Player, Inventory

@strawberry.type
class PlayersQuery:

    @strawberry.field
    def player_profile(self, id: str) -> Optional[Player]:
        doc = db.collection("Player").document(id).get()

        if doc.exists: 
            data = doc.to_dict()
            return Player(id=doc.id, **data)
        
        return None