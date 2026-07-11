# Tercer Projecte

This project objective was running a game from a react native frontend. That react native frontend would be an intermediary between the server (a FastApi + GraphQL) and the Unity Game.

At the current state, the backend is fully operable (Works via Firebase) and the Game is playable, but due to personal limitations, the frontend has been simplified to the bare minimum.

## Game

Controls: 
* A/D Keys or <-/-> arrows to move the spaceship
* Left Click to shoot

## Firebase User (Required at frontend login):

info@mail.com

123456

## Frontend

Frontend is located at /Frontend

once at the frontend directory, run "npm install" to install the node dependencies.

to access the website, run "npm run web" from the terminal. (Having the backend running is required for the game and the requests to work)

## Backend

Backend is located at /PinguAPI

a python virtual environment inside the backend directory with the requirements.txt installed via pip is required.

Once the virtual environment is started, run the server by running main.py

## GraphQL Queries & Mutations

### Perfil Jugador
```
query Perfil_Jugador {
  playerProfile(id: "CTZ9dxYc8LQsTeIURWclUyoAgEB3") {
    id,
    nickname,
    lvl,
    banned,
    inventory {
      id,
      itemName,
      rarity
    }
  }
}
```

### Llistar Partides
```
query Llistar_Partides {
  getGames {
    id
    map
    state
    createdOn
  }
}

query Llistar_Partides_Filtrat_Mapa {
  getGames(map: "The Moon") {
    id
    map
    state
    createdOn
  }
}

query Llistar_Partides_Filtrat_Estat {
  getGames(state: "Playing") {
    id
    map
    state
    createdOn
  }
}

query Llistar_Partides_Filtrat_Limit_Offset {
  getGames(offset: 2, limit: 1) {
    id
    map
    state
    createdOn
  }
}
```

### Taula Classificació

```
query Taula_Classificacio {
  getScores(gameId: "Po9hfddP3smIMMz1c1b3") {
    id
    playerId
    playerName
    points
    kills
  }
}
```

### Registrar Jugador (Requereix Header)

```
mutation Registrar_Jugador{
  registerPlayer(data:{
    nickname:"Merequetengue"
  })
  {
    id,
    nickname,
    banned,
    lvl,
    inventory{
      id,
      itemName,
      rarity
    }
  }
}
```

### Crear Partida

```
mutation Crear_Partida {
  createGame(data: {
    map: "La Ferreria"
  }){
    id,
    map,
    state,
    createdOn    
  }
}
```

### Atorgar Item

```
mutation Atorgar_Item{
  giveItem(data: {
    userId: "CTZ9dxYc8LQsTeIURWclUyoAgEB3",
    itemName: "The Thing",
    rarity: "Special Week"
  }){
    id,
    itemName,
    rarity
  }
}
```
### Registrar Puntuació

```
mutation Registrar_Puntuacio{
  registerScore(data: {
    gameId: "Po9hfddP3smIMMz1c1b3",
    playerId:"CTZ9dxYc8LQsTeIURWclUyoAgEB3",
    points: "7",
  	kills: "5"
  }){
    id,
    playerId,
    points,
    kills
  }
}
```

### Finalitzar Partida

```
mutation Finalitzar_Partida {
  endGame(gameId: "dRLaCkoxiWqnQ1iVywoA"){
    id,
    map,
    state,
    createdOn,
    scores {
      id,
      playerId,
      points
      kills
    }
  }
}
```
