import { Pressable, Text, View, StyleSheet } from 'react-native';
import { API_URL } from '../config';
import { useApp } from '../AppContext';

export default function Profile() {

    const { setNickname } = useApp();

    // TODO: This here or in the homepage???
    const getPlayerInfo = async () => {
        try {
            const response = await fetch({ API_URL }, {
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

            const data = await response.json();

            console.log(data)
            
            // TODO: Set data?
            // setNickname()

        } catch (error) {
            console.error(error)
        }
    }

    return (
        <View style={styles.container}>
            <Text>Nickname</Text>
            <Text>Money: 90 €</Text>

            {/* TODO: Show User Name & money? */}

            {/* TODO: getScores by level???? */}

            {/* <Pressable onPressIn={getPlayerInfo} style={styles.button}>
                <Text>Get Player Info</Text>
            </Pressable> */}
        </View>
    )
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        flexDirection: 'row',
        gap: 20,
        backgroundColor: '#dfdfdfff',
        borderRadius: 5,
    },
})