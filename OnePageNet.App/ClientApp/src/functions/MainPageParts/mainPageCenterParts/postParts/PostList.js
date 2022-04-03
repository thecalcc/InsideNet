import React, { useState, useEffect } from "react";
import { PostListItem } from "./PostListItem";
import "../../../styles/PostList.css"
export function PostList({onClick, onSelect, users, deletePost}) {
  const [posts, setPosts] = useState();

  useEffect(() => {
    const fetchPosts = async () => {
      const urlPosts = "https://localhost:7231/api/Posts/get-all";
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
    };

    fetchPosts();
  }, []);

  const getPosterName = (posterId) => {
    return users?.find((user) => user.id === posterId).userName;
  };

  return (
    <ul className = "post-list">
      {posts?.map((post) => {
        return (
          <li className="post">
          <PostListItem
            post={post}
            selectPost={onSelect}
            poster={getPosterName(post.posterId)}
            deletePost = {deletePost}
          />
          <div>
            <button onClick={() => onClick(post)}>Comments</button>
          </div>
        </li>
        );
      })}
    </ul>
  );
}
