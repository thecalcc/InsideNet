import React from "react";
import { Route } from "react-router";
import { Layout } from "./functions/Layout";
import { Home } from "./functions/Home";
import { Register } from "./functions/Register";
import { Login } from "./functions/Login";
import "./custom.css";
import useToken from "./functions/useToken";

export function App(props) {
  const { token, setToken } = useToken();

  return (
    <Layout>
      <Route exact path="/">
        <Home name={props.name} />
      </Route>
      {token === true ? (
        <Route exact path="/">
          <Home name={props.name} />
        </Route>
      ) : (
        <>
          <Route exact path="/register">
            <Register setToken={setToken} />
          </Route>
          <Route exact path="/login">
            <Login setToken={setToken} />
          </Route>
        </>
      )}
    </Layout>
  );
}
