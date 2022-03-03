import React from "react";
import { NavMenu } from "./NavMenu";
import { Route } from "react-router";
import { Home } from "./Home";
import { Register } from "./Register";
import { Login } from "./Login";

export function Layout({ setToken, token }) {
  return (
    <div>
      <NavMenu token={token} setToken={setToken} />
      {token === null || token === "" || token === undefined ? (
        <>
          <Route exact path="/">
            <Home />
          </Route>
          <Route exact path="/register">
            <Register token={token} setToken={setToken} />
          </Route>
          <Route exact path="/login">
            <Login token={token} setToken={setToken} />
          </Route>
        </>
      ) : (
        <>
          <Route exact path="/">
            <Home />
          </Route>
        </>
      )}{" "}
    </div>
  );
}
