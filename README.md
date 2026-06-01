# Tercer Projecte

## Joc

Controls: 
* Tecles A/D o fletxa esquerra/dreta pel moviment
* Click Esquerra per disparar

## User de firebase (Requerit al login del frontend):

info@mail.com

123456

## Frontend

El frontend es troba al directori /Frontend

amb el directori obert des d'un IDE, executar "npm install" per instal·lar les dependecies.

per accedir al web executar "npm run web" al terminal. (Per carregar tant el joc com les consultes a l'API cal executar el backend previament)

## Backend

El Backend es troba a /PinguAPI

cal crear per terminal un virtual environment de python dins el directori del backend i instal·lar el requirements.txt mitjançant pip.

Una vegada iniciat el virtual environment, arrencar el servidor executant el main.py

## Consultes GraphQL

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
