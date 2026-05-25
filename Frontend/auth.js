import { signInWithEmailAndPassword } from 'firebase/auth';
import { auth } from '../firebase';

export async function login(email, password) {
  const userCredential =
    await signInWithEmailAndPassword(
      auth,
      email,
      password
    );

  return userCredential.user;
}

export async function getToken() {
  const user = auth.currentUser;

  if (!user) {
    throw new Error('Not authenticated');
  }

  return await user.getIdToken();
}