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
  const [layoutState, setLayout] = useState({left: '', center: 'timeline', right: ''});
  const [ rerender, setRerender ] = useState(true);


  useEffect(() => {
    return () => {
      setToken(sessionStorage.getItem("token"));
    };
  }, []);

  const onLayoutChange = (layout, root) => {
    let tmp = layoutState;
    switch(root){
      case "left":
        tmp.left = layout
        setLayout(tmp);
        break;
      case "center":
        tmp.center = layout
        setLayout(tmp);
        break
      case "right":
        tmp.right = layout
        setLayout(tmp);
        break
    }
    setRerender(!rerender)
  }

  return (
    <div>
      {console.log("braindamage")}
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
