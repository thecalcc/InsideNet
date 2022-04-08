/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable default-case */
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
  const [layoutState, setLayout] = useState({left: "groupSelection", center: "", right: ""});
  const shouldHaveUsedRerender = () => {
    if((sessionStorage.getItem("currentUserId")&& layoutState.center === "")){
      setLayout({...layoutState,center: "timeline"})}
  }

  shouldHaveUsedRerender();
  useEffect(() => {
    return () => {
      setToken(sessionStorage.getItem("token"));
    };
  }, []);

  const onLayoutChange = (layout, root) => {
    switch(root){
      case "left":
        setLayout({...layoutState,left: layout});
        break;
      case "center":
        setLayout({...layoutState,center: layout});
        break
      case "right":
        setLayout({...layoutState,right: layout});
        break
    }
  }
  const [posts, setPosts] = useState();
  useEffect(()=>{ const fetchPosts = async () => {
      const urlPosts = `https://localhost:7231/api/Posts/get-timeline/${sessionStorage.getItem("currentUserId")}`;
      await fetch(urlPosts, {
        method: "GET",
        mode: "cors",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
          "Access-Control-Allow-Origin": "*",
        },
      })
        .then((data) => data.json())
        .then((data) => setPosts(data));
    }

    if (token !== undefined && token !== null) {
      fetchPosts();
    }
  },[layoutState])

  return (
    <div>
      {token === null ? (
        <>
          <Route exact path="/">
            <Home setToken={setToken} onLayoutChange={onLayoutChange}/>
          </Route>
        </>
      ) : (
        <>
        <NavMenu setToken={setToken} token={token} onLayoutChange={onLayoutChange}/>
          <Route exact path="/">
            <MainPage currentLayout={layoutState} onLayoutChange={onLayoutChange} posts={posts}/>
          </Route>
        </>
      )}
    </div>
  );
}
