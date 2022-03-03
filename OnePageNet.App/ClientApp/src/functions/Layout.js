import React, { useState } from "react";
import { NavMenu } from "./NavMenu";
import { Route } from "react-router";
import { Home } from "./Home";
import { Register } from "./Register";
import { Login } from "./Login";
import useToken from "./useToken";

export function Layout() {
  const { token, setToken } = useToken();

  return (
    <div>
      <NavMenu setToken={setToken} token={token} />
      {token != "" ? (
        <>
          <Route exact path="/">
            <Home />
          </Route>
          <Route exact path="/register">
            <Register setToken={setToken} />
          </Route>
          <Route exact path="/login">
            <Login setToken={setToken} />
          </Route>
        </>
      ) : (
        <>
          <Route exact path="/">
            <Home />
          </Route>
        </>
      )}
    </div>
  );
}
