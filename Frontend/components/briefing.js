import Video from 'react-native-video';
import { View } from 'react-native-web';
import { styles } from '../styles';

const Briefing = () => {

    const video = require('../assets/briefing.mp4');

    {/* TODO: Fix this and show it in a modal */}

    return (
        <View>
            <Video style={styles.video}
                source={video}
                paused={false}
            />
        </View>
    )
}

export default Briefing;