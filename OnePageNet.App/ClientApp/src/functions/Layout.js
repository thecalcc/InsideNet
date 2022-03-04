import React, { useState } from "react";
import { NavMenu } from "./NavMenu";
import { Route } from "react-router";
import { Home } from "./Home";
import { Register } from "./Register";
import { Login } from "./Login";
import { UserList } from "./UserList";
import { MainPage } from "./MainPage";
import useToken from "./useToken";



export function Layout() {
  const { token, setToken } = useToken();

  return (
    <div>
      {token == null ? (
        <>
          <Route exact path="/">
            <Home />
          </Route>
          <Route exact path="/register">
            <Register setToken={setToken} />
          </Route>
          <Route exact path="/authentication/get-all">
            <UserList setToken={setToken} />
          </Route>
          <Route exact path="/login">
            <Login setToken={setToken} />
          </Route>
        </>
      ) : (
        <>
          <NavMenu setToken={setToken} token={token} />
          <Route exact path="/">
            <MainPage />
          </Route>
        </>
      )}
    </div>
  );
}
