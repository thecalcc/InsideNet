import React, { useState, useEffect } from 'react';
import { MainPageLeft } from './mainPageParts/MainPageLeft.js';
import { MainPageCenter } from './mainPageParts/MainPageCenter.js';
import { MainPageRight } from './mainPageParts/MainPageRight.js';
import "./styles/MainPage.css"

export function MainPage({ currentLayout, onLayoutChange, posts }) {
  const [post, setPost] = useState(null);
  const [users, setUsers] = useState();
  const selectPost = (post) => {
    setPost(post);
  };
  useEffect(() => {
    const fetchUsers = async () => {
      const urlUsers = "https://localhost:7231/api/Users/get-all";
      await fetch(urlUsers, {
        method: "GET",
        mode: "cors",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
          "Access-Control-Allow-Origin": "*",
        },
      })
        .then((data) => data.json())
        .then((data) => setUsers(data));
    };
    fetchUsers();
  }, []);

  return (
    <>
      <div className="main-page">
        <MainPageLeft
          layoutState={currentLayout.left}
          onLayoutChange={onLayoutChange}
        />
        <MainPageCenter
          layoutState={currentLayout.center}
          onLayoutChange={onLayoutChange}
          selectPost={selectPost}
          users={users}
          posts={posts}
        />
        <MainPageRight
          layoutState={currentLayout.right}
          post={post}
          users={users}
          onLayoutChange={onLayoutChange}
          changePost={selectPost}
        />
      </div>
    </>
  );
};