import React from "react";
import { AppProvider } from "./AppContext";
import HomePage from './homepage';

export default function App() {
  return (
    <AppProvider>
      <HomePage />
    </AppProvider>
  )
}