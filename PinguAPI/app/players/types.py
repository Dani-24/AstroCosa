import strawberry
from typing import List, Optional

@strawberry.type
class Inventory:
    id: str
    item_name: str
    rarity: str

@strawberry.type
class Player:
    id: str
    nickname: str
    lvl: int
    banned: bool
    inventory: Optional[List[Inventory]] = None

@strawberry.input
class PlayerInput:
    nickname: str

@strawberry.input
class InventoryInput:
    user_id: str
    item_name: str
    rarity: str