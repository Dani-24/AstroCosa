import { Text, View } from 'react-native';
import { styles } from '../styles';

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