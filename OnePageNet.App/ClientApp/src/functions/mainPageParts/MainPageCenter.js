import React, { useState, useEffect } from "react";
import "../styles/MainPage.css";
import { PostList } from "./mainPageCenterParts/postParts/PostList";
import { PostListItem } from "./mainPageCenterParts/postParts/PostListItem";
import { CreatePost } from "./mainPageCenterParts/postParts/CreatePost";
import { EditPost } from "./mainPageCenterParts/postParts/EditPost";
import { CommentList } from "./mainPageCenterParts/commentParts/CommentList";
import { CreateComment } from "./mainPageCenterParts/commentParts/CreateComment";

export function MainPageCenter({onLayoutChange}) {
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

  const onPostClick = (post) => {
    selectPost(post, "comments");
    onLayoutChange("post", "right");
  }


  return (
    <>
      {currentPost == null || currentPost == undefined ? (
        createPost == false ? (
          <div className="main-page-center">
            <button onClick={() => setCreatePost(true)}>Create post</button>
            <PostList
              onClick={onPostClick}
              onSelect={selectPost}
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
          <CreateComment idOfPost = {currentPost.id}/>
          <CommentList idOfPost = {currentPost.id}/>
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
