import React, { useEffect, useState } from "react";
import { NavMenu } from "./NavMenu";
import { Route } from "react-router";
import { Home } from "./Home";
import { Register } from "./Register";
import { Login } from "./Login";
import { MainPage } from "./MainPage";
import useToken from "./useToken";

export function Layout() {
  const { token, setToken } = useToken();
    const [layoutState, setLayout] = useState('');

  useEffect(() => {
    return () => {
      setToken(sessionStorage.getItem("token"));
    };
  }, []);

  const onLayoutChange = (layout) => {
    setLayout(layout)
  }

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
        <NavMenu setToken={setToken} token={token} onLayoutChange={onLayoutChange}/>
          <Route exact path="/">
            <MainPage currentLayout={layoutState} onLayoutChange={onLayoutChange}/>
          </Route>
        </>
      )}
    </div>
  );
}
