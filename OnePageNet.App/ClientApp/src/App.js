import React from "react";
import { Layout } from "./functions/Layout";
import "./custom.css";
import useToken from "./functions/useToken";

export function App() {
  const { token, setToken } = useToken();

  return <Layout token={token} setToken={setToken} />;
}
