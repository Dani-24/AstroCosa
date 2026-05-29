import { StatusBar } from 'expo-status-bar';
import { Text, View, Pressable, Image } from 'react-native';
import Radar from './components/radar';
import PowerUps from './components/powerups';
import Briefing from './components/briefing';
import Profile from './components/profile';
import { styles } from './styles';
// import 'bootswatch/dist/vapor/bootstrap.min.css';
import { TextInput } from 'react-native-web';
import { useState } from 'react';


export default function App() {

  const { nickname, onNickname } = useState("");

  const registerPlayer = async () => {
    const token = await getToken();
    const res = await fetch('http://127.0.0.1:8080/graphql', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`,
      },
      body: JSON.stringify({
        query: `
          mutation {
            registerPlayer(
              data: {
                nickname: "${nickname}"
              }
            ) { 
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
    });

    const data = await res.json()
    console.log('data returned:', data);
  }

  return (
    <View style={styles.container}>
      <Text>Projecte 3</Text>
      <StatusBar style="auto" />

      <Pressable onPressIn={registerPlayer}>
        <Text>Register Player</Text>
      </Pressable>
      <TextInput
        onChangeText={onNickname}
        value={nickname}
        style={styles.button} />

      <Image source={require("./assets/icon.png")} style={{ width: 40, height: 40 }}></Image>

      <Profile />
      <PowerUps />
      <View style={styles.container}>
        <Briefing />
      </View>
      <Radar />
    </View>
  );
}