import React, { useEffect, useState } from "react";
import { NavMenu } from "./NavMenu";
import { Route } from "react-router";
import { Home } from "./Home";
import { Register } from "./Register";
import { Login } from "./Login";
import { MainPage } from "./MainPage";
import useToken from "./useToken";
import { Users } from "./mainPageParts/Users";

export function Layout() {
  const { token, setToken } = useToken();
  const [temp, setTemp] = useState();

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
          <NavMenu setToken={setToken} token={token} temp = {temp} setTemp = {setTemp}/>
          <Route exact path="/">
            <MainPage temp = {temp} setTemp = {setTemp}/>
          </Route>
        </>
      )}
    </div>
  );
}
