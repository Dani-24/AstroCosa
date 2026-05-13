import { Text, View } from 'react-native';
import { styles } from '../styles';

export default function Radar() {

    return (
        <View style={styles.container}>
            <Text>Canvas</Text>
            <canvas/>
        </View>
    )

}