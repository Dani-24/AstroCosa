import strawberry

from games.queries import GamesQuery
from games.mutations import GamesMutations

from players.queries import PlayersQuery
from players.mutations import PlayersMutations

@strawberry.type
class Query(GamesQuery, PlayersQuery):
    pass

@strawberry.type
class Mutation(GamesMutations, PlayersMutations):
    pass