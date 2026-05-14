import { StatusBar } from 'expo-status-bar';
import { Text, View, Pressable, Image } from 'react-native';
import Radar from './components/radar';
import PowerUps from './components/powerups';
import Briefing from './components/briefing';
import Profile from './components/profile';
import { styles } from './styles';
import 'bootswatch/dist/vapor/bootstrap.min.css';
import { TextInput } from 'react-native-web';
import { useState } from 'react';

export default function App() {

  const {newPlayerName, onNewPlayerName} = useState("");

  const getPlayerInfo = () => {
    try {
      fetch('http://127.0.0.1:8080/graphql', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          query: `{
          playerProfile(id: "CTZ9dxYc8LQsTeIURWclUyoAgEB3") { 
            id, 
            nickname 
            lvl,
            banned,
            inventory {
              id,
              itemName ,
              rarity
              }
            }
          }`,
        })
      })
        .then(r => r.json())
        .then(data => console.log('data returned:', data));
    } catch (error) {
      console.error(error)
    }
  }

  const registerPlayer = () => {
    try {
      fetch('http://127.0.0.1:8080/graphql', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          query: `type Mutation {
          registerPlayer(data: "$Name") { 
            id, 
            nickname 
            lvl,
            banned,
            inventory {
              id,
              itemName ,
              rarity
              }
            }
          }`,
        })
      })
        .then(r => r.json())
        .then(data => console.log('data returned:', data));
    } catch (error) {
      console.error(error)
    }
  }

  return (
    <View style={styles.container}>
      <Text>Mondongo</Text>
      <StatusBar style="auto" />

      <Pressable onPressIn={getPlayerInfo}>
        <Text>Get Player Info</Text>
      </Pressable>

      <Pressable onPressIn={registerPlayer}>
        <Text>Register Player</Text>
      </Pressable>
      <TextInput 
        onChangeText={onNewPlayerName}
        value={newPlayerName}
        style={{borderWidth:1}}/>

      <Image source={require("./assets/icon.png")} style={{ width: 40, height: 40 }}></Image>

      <Profile />
      <PowerUps />
      <Briefing />
      <Radar />

    </View>
  );
}