import React from "react";
import { AppProvider } from "./AppContext";
import Login from "./login";

export default function App() {
  return (
    <AppProvider>
      <Login />
    </AppProvider>
  )
}