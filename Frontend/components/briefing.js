import React, { useState } from 'react';
import { Modal, View, TouchableOpacity, Text, StyleSheet } from 'react-native';
// import Video from 'react-native-video';
import briefingVideo from '../assets/briefing.mp4';

export default function Briefing() {
    const [visible, setVisible] = useState(false);

    return (
        <>
            <TouchableOpacity
                style={styles.openButton}
                onPress={() => setVisible(true)}
            >
                <Text style={styles.buttonText}>Play Video</Text>
            </TouchableOpacity>

            <Modal
                visible={visible}
                animationType="slide"
                presentationStyle="fullScreen"
                onRequestClose={() => setVisible(false)}
            >
                <View style={styles.container}>
                    <TouchableOpacity
                        style={styles.closeButton}
                        onPress={() => setVisible(false)}
                    >
                        <Text style={styles.closeText}>✕</Text>
                    </TouchableOpacity>

                    <video
                        controls
                        autoPlay
                        style={{
                            aspectRatio: '16 / 9',
                        }}
                    >
                        <source src={briefingVideo} type="video/mp4" />
                    </video>
                </View>
            </Modal>
        </>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: 'black',
        justifyContent: 'center',
    },
    video: {
        width: '100%',
        height: 300,
    },
    closeButton: {
        position: 'absolute',
        top: 60,
        right: 20,
        zIndex: 10,
    },
    closeText: {
        color: 'white',
        fontSize: 28,
    },
    openButton: {
        padding: 12,
        backgroundColor: '#007AFF',
        borderRadius: 8,
    },
    buttonText: {
        color: 'white',
        fontWeight: '600',
    },
});