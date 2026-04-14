import strawberry
from .types import Player, Inventory, PlayerInput, InventoryInput
from typing import List, Optional
from firebase_conf import db

@strawberry.type
class PlayersMutations:

    @strawberry.mutation
    def register_player(self, info: strawberry.Info, data: PlayerInput) -> Player:

        user = info.context["user"]

        if not user:
            raise Exception("Operation Denied: User is not authenticated")

        player_data = {
            "nickname": data.nickname,
            "lvl": 1,
            "banned": False
        }

        ref = db.collection("Player").document(user.get("uid"))
        ref.set(player_data)

        return Player(id=ref.id, **player_data)

    @strawberry.mutation
    def give_item(self, data: InventoryInput) -> Inventory:

        player_ref = db.collection("Player").document(data.user_id)
        item_ref = player_ref.collection("Inventory").document()

        item_data = {
            "item_name": data.item_name,
            "rarity": data.rarity,
        }

        item_ref.set(item_data)

        return Inventory(id=item_ref.id, **item_data)