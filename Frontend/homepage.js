import { StatusBar } from 'expo-status-bar';
import { Text, View, Pressable, Image, StyleSheet } from 'react-native';
import Radar from './components/radar';
import PowerUps from './components/powerups';
import Briefing from './components/briefing';
import Profile from './components/profile';
// import 'bootswatch/dist/vapor/bootstrap.min.css';
import { TextInput } from 'react-native-web';
import { useState } from 'react';
import TheGame from './components/game';
import { API_URL } from './config';


export default function HomePage() {

    const [isPlaying, setIsPlaying] = useState(false);

    return (
        <View style={styles.container}>
            <View style={styles.profileContainer}>
                <Profile />
            </View>

            <View style={styles.mainRow}>
                <View style={styles.leftColumn}>
                    <PowerUps />
                </View>

                <View style={styles.rightColumn}>
                    <Radar />

                    <View style={styles.bottomRow}>
                        <View style={styles.briefingContainer}>
                            <Briefing />
                        </View>

                        <Pressable
                            style={styles.playButton}
                            onPress={() => setIsPlaying(true)}>
                            <Text>Play</Text>
                        </Pressable>
                    </View>
                </View>
            </View>

            {isPlaying && (
                <View style={styles.gameOverlay}>
                    <TheGame />

                    <Pressable
                        style={styles.exitButton}
                        onPress={() => setIsPlaying(false)}
                    >
                        <Text>Exit</Text>
                    </Pressable>
                </View>
            )}
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20,
    },

    profileContainer: {
        alignSelf: 'flex-end',
        marginBottom: 20,
    },

    mainRow: {
        flexDirection: 'row',
        flex: 1,
        gap: 20,
    },

    leftColumn: {
        flex: 1,
    },

    rightColumn: {
        flex: 1,
        justifyContent: 'flex-start',
    },

    bottomRow: {
        flexDirection: 'row',
        alignItems: 'center',
        gap: 16,
        marginTop: 20,
    },

    briefingContainer: {
        flex: 1,
    },

    playButton: {
        paddingHorizontal: 24,
        paddingVertical: 12,
        backgroundColor: '#69e749ff',
        borderRadius: 8,
        alignItems: 'center',
        justifyContent: 'center',
    },

    gameOverlay: {
        position: "absolute",
        top: 0,
        left: 0,
        right: 0,
        bottom: 0,
        backgroundColor: "black",
        zIndex: 999,
    },

    exitButton: {
        position: "absolute",
        borderRadius: 8,
        backgroundColor: '#eb2d2dff',
        top: 40,
        right: 20,
        zIndex: 1000,
        padding: 10
    },
});