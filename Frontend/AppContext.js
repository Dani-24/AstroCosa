import React, { createContext, useContext, useState } from "react";

const AppContext = createContext();

export function AppProvider({ children }) {
  const [token, setToken] = useState(null);
  const [nickname, setNickname] = useState("");
  const [selectedMap, setSelectedMap] = useState(null);

  return (
    <AppContext.Provider
      value={{
        token,
        setToken,
        nickname,
        setNickname,
        selectedMap,
        setSelectedMap,
      }}
    >
      {children}
    </AppContext.Provider>
  );
}

export function useApp() {
  return useContext(AppContext);
}