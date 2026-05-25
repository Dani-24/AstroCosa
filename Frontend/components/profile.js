import { Pressable, Text, View } from 'react-native';
import { styles } from '../styles';


export default function Profile() {

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

    return (
        <View style={styles.container}>
            <Text>Profile</Text>

            <Pressable onPressIn={getPlayerInfo}>
                <Text>Get Player Info</Text>
            </Pressable>
        </View>
    )

}