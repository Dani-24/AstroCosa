import strawberry
from .types import Player, Inventory

@strawberry.type
class PlayersMutations:
    
    @strawberry.mutation
    def register_player(self) -> Player:
        # TODO, get ID from JWT context
        pass

    @strawberry.mutation
    def give_item(self) -> Inventory:
        # TODO, Add an item to the inventory of a player
        pass