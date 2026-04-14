import strawberry

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
    inventory: Inventory