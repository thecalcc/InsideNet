import React, { useState, useEffect } from "react";
import "../styles/MainPage.css";
import { PostList } from "./MainePageCenterParts/PostList";
import { PostListItem } from "./MainePageCenterParts/PostListItem";
import { CreatePost } from "./MainePageCenterParts/CreatePost";
import { EditPost } from "./MainePageCenterParts/EditPost";

export function MainPageCenter() {
  const [currentPost, setCurrentPost] = useState();
  const [createPost, setCreatePost] = useState();
  const [users, setUsers] = useState();
  const [action, setAction] = useState();

  const selectPost = (post, action) => {
    setCurrentPost(post);
    setAction(action);
  };
  const isMyPost = (posterId) => {
    return posterId == sessionStorage.currentUserId;
  };
  const deletePost = async (id) => {
    const urlUsers = `https://localhost:7231/api/posts/delete/${id}`;
    await fetch(urlUsers, {
      method: "DELETE",
      mode: "cors",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        "Access-Control-Allow-Origin": "*",
      },
    });
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

  const getPosterName = (posterId) => {
    return users?.find((user) => user.id === posterId).userName;
  };

  return (
    <>
      {currentPost == null || currentPost == undefined ? (
        createPost == false ? (
          <div className="main-page-center">
            <button onClick={() => setCreatePost(true)}>Create post</button>
            <PostList
              selectPost={selectPost}
              users={users}
              deletePost={deletePost}
            />
          </div>
        ) : (
          <>
            <CreatePost />
            <button onClick={() => setCreatePost(false)}>Back</button>
          </>
        )
      ) : action === "comments" ? (
        <div className="post">
          <PostListItem
            selectPost={selectPost}
            post={currentPost}
            poster={getPosterName(currentPost.posterId)}
            isMyPost={isMyPost(currentPost.posterId)}
            deletePost={deletePost}
          />
          <button onClick={() => setCurrentPost(null)}>Back</button>
        </div>
      ) : (
        <div>
          <EditPost post={currentPost} />
          <button onClick={() => setCurrentPost(null)}>Back</button>
        </div>
      )}
    </>
  );
}
