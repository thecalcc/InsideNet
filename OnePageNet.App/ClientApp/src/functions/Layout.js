import React, { useEffect } from "react";
import { NavMenu } from "./NavMenu";
import { Route } from "react-router";
import { Home } from "./Home";
import { Register } from "./Register";
import { Login } from "./Login";
import { MainPage } from "./MainPage";
import useToken from "./useToken";
import { Users } from "./MainPageParts/Users";

export function Layout() {
  const { token, setToken } = useToken();

  useEffect(() => {
    return () => {
      setToken(sessionStorage.getItem("token"));
    };
  }, []);

  return (
    <div>
      {token === null ? (
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
          <NavMenu setToken={setToken} token={token} />
          <Route exact path="/users">
            <Users />
          </Route>
          <Route exact path="/">
            <MainPage />
          </Route>
        </>
      )}
    </div>
  );
}
