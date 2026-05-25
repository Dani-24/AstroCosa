import { Pressable, View } from 'react-native';
import { styles } from '../styles';

import { login } from '../auth';
import { useState } from 'react';
import { Text, TextInput } from 'react-native-web';

export default function Login() {

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    async function handleLogin() {
        try {
            const user = await login(email, password);

            console.log('Logged in:', user.email);

        } catch (err) {
            console.error(err);
        }
    }

    return (
        <View style={styles.container}>
            <TextInput
                placeholder="Email"
                value={email}
                onChangeText={setEmail}
            />

            <TextInput
                placeholder="Password"
                secureTextEntry
                value={password}
                onChangeText={setPassword}
            />

            <Pressable onPressIn={handleLogin}>
                <Text>Login</Text>
            </Pressable>

        </View>
    );

}