import React, { useState, useEffect } from "react";
import "../styles/MainPage.css";
import { PostList } from "./MainePageCenterParts/PostList";
import { PostListItem } from "./MainePageCenterParts/PostListItem";
import { CreatePost } from "./MainePageCenterParts/CreatePost";

export function MainPageCenter() {
  const [currentPost, setCurrentPost] = useState();
  const [createPost, setCreatePost] = useState();
  const [users, setUsers] = useState();
  const selectPost = (e) => {
    setCurrentPost({ e });
  };
  const isMyPost = (posterId) =>{
    return (posterId == sessionStorage.currentUserId);
  }
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

  const getPosterName = (posterId) => {
    return users?.find((user) => user.id === posterId).userName;
  };

  return (
    <>
      {currentPost == null || currentPost == undefined ? (
        createPost == false ? (
          <div className="main-page-center">
            <button onClick={() => setCreatePost(true)}>Create post</button>
            <PostList selectPost={selectPost} users = {users}/>
          </div>
        ) : (
          <>
            <CreatePost />
            <button onClick={() => setCreatePost(false)}>Back</button>
          </>
        )
      ) : (
        <div className="post">
          <PostListItem
            selectPost={selectPost}
            post={currentPost.e}
            poster={getPosterName(currentPost.e.posterId)}
            isMyPost = {isMyPost(currentPost.e.posterId)}           
          />
          <button onClick={() => setCurrentPost(null)}>Back</button>
        </div>
      )}
    </>
  );
}
