import { Text, View, StyleSheet, Image } from 'react-native';

export default function PowerUps() {

    const powerUpsData = [
        {
            id: 1,
            name: "Powerup name",
            description: "Power up description",
            image: require("../assets/icon.png"),
        },
        {
            id: 2,
            name: "Powerup name",
            description: "Power up description",
            image: require("../assets/icon.png"),
        },
        {
            id: 3,
            name: "Powerup name",
            description: "Power up description",
            image: require("../assets/icon.png"),
        },
        {
            id: 4,
            name: "Powerup name",
            description: "Power up description",
            image: require("../assets/icon.png"),
        },
        {
            id: 5,
            name: "Powerup name",
            description: "Power up description",
            image: require("../assets/icon.png"),
        },
        {
            id: 6,
            name: "Powerup name",
            description: "Power up description",
            image: require("../assets/icon.png"),
        },
    ];

    return (
        <View style={styles.container}>
            <Text style={styles.title}>PowerUps</Text>

            <View style={styles.grid}>
                {powerUpsData.map((item) => (
                    <View key={item.id} style={styles.card}>
                        <Image source={item.image} style={styles.image} />
                        <Text style={styles.name}>{item.name}</Text>
                        <Text style={styles.desc}>{item.description}</Text>
                    </View>
                ))}
            </View>
        </View>
    );

}

const styles = StyleSheet.create({
    container: {
        flex: 1,
    },

    title: {
        fontSize: 18,
        fontWeight: "bold",
        marginBottom: 10,
    },

    grid: {
        flexDirection: "row",
        flexWrap: "wrap",
        justifyContent: "space-between",
    },

    card: {
        width: "49%",
        backgroundColor: "#eee",
        padding: 20,
        marginBottom: 20,
        borderRadius: 500,
        alignItems: "center",
    },

    image: {
        width: 40,
        height: 40,
        marginBottom: 5,
    },

    name: {
        fontWeight: "bold",
        textAlign: "center",
    },

    desc: {
        fontSize: 12,
        textAlign: "center",
    },
});