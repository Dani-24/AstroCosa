import { Pressable, Text, View } from 'react-native';
import { styles } from '../styles';

export default function Radar() {

    function draw() {
        try {
            const canvas = document.getElementById("canvas");
            const ctx = canvas.getContext("2d");

            ctx.fillStyle = "rgb(200 0 0)";
            ctx.fillRect(10, 10, 50, 50);

            ctx.fillStyle = "rgb(0 0 200 / 50%)";
            ctx.fillRect(30, 30, 50, 50);
        } catch (Error) {
            console.log("A " + Error)
        }
    }
    draw();

    return (
        <View style={styles.container}>
            <Text>Canvas</Text>
            <Pressable onPressIn={draw} style={styles.button}><Text>Pulsa</Text></Pressable>
            <canvas id="canvas" width="150" height="150" />
        </View>
    )

}