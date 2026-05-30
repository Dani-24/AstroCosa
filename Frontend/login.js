import HomePage from './homepage';

import { FIREBASE_API_KEY } from "./config";
import { useApp } from "./AppContext";
import LoginModal from "./components/loginModal";


export default function Login() {
    
    const {token, setToken} = useApp();
    
    async function login(email, password) {
        const response = await fetch(
            `https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=${FIREBASE_API_KEY}`,
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({
                    email,
                    password,
                    returnSecureToken: true,
                }),
            }
        );
    
        const data = await response.json();
    
        setToken(data.idToken);
    }

    return (
        <>
            <LoginModal
                visible={!token}
                onLogin={login}
            />

            {token && <HomePage />}
        </>
    )
}