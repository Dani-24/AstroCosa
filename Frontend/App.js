import { StatusBar } from 'expo-status-bar';
import { Text, View } from 'react-native';
import Radar from './components/radar';
import PowerUps from './components/powerups';
import Briefing from './components/briefing';
import Profile from './components/profile';
import { styles } from './styles';

export default function App() {
  return (
    <View style={styles.container}>
      <Text>Mondongo</Text>
      <StatusBar style="auto" />

      <Profile />
      <PowerUps />
      <Briefing />
      <Radar />

    </View>
  );
}