import { Platform } from 'react-native';

export default function TheGame() {

    const aspectRatio = 9 / 16;
    const gameHeight = "100vh";
    const gameWidth = `calc(100vh * ${aspectRatio})`;

    if (Platform.OS !== "web") {
        return null;
    }

    return (
        <div
            style={{
                height: gameHeight,
                width: gameWidth,
                alignSelf: 'center',
            }}
        >
            <iframe
                src="http://127.0.0.1:8080/unity/index.html"
                style={{
                    width: '100%',
                    height: '100%',
                    border: 'none',
                }}
            />
        </div>
    );
}