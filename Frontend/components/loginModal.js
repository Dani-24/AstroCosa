import { useState } from "react";
import {
  Modal,
  View,
  Text,
  TextInput,
  Pressable,
} from "react-native";

export default function LoginModal({ visible, onLogin }) {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  return (
    <Modal visible={visible} transparent>
      <View
        style={{
          flex: 1,
          justifyContent: "center",
          alignItems: "center",
          backgroundColor: "rgba(0,0,0,0.5)",
        }}
      >
        <View
          style={{
            width: 300,
            padding: 20,
            backgroundColor: "white",
            borderRadius: 8,
          }}
        >
          <Text>Email</Text>

          <TextInput
            value={email}
            onChangeText={setEmail}
            autoCapitalize="none"
            style={{
              borderWidth: 1,
              marginBottom: 10,
              padding: 8,
            }}
          />

          <Text>Password</Text>

          <TextInput
            value={password}
            onChangeText={setPassword}
            secureTextEntry
            style={{
              borderWidth: 1,
              marginBottom: 20,
              padding: 8,
            }}
          />

          <Pressable
            onPress={() => onLogin(email, password)}
          >
            <Text>Login</Text>
          </Pressable>
        </View>
      </View>
    </Modal>
  );
}