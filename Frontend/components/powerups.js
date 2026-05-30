import { Text, View, StyleSheet } from 'react-native';

export default function PowerUps() {

    return (
        <View style={styles.container}>
            <Text>PowerUps</Text>

            {/* TODO: Show items list asigned to this player 
                query playerProfile -> Inventory
            */}
        </View>
    )

}

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
})