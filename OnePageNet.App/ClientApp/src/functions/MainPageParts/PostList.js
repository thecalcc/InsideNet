import React, { useState, useEffect } from "react";
import { PostListItem } from "./PostListItem";

export function PostList() {
  const [users, setUsers] = useState();
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
    fetchPosts();
  }, []);

  const getPosterName = (posterId) => {
    return users?.find((user) => user.id === posterId).userName;
  };

  return (
    <ul>
      {posts?.map((post) => {
        return (
          <PostListItem
            createdAt={post.createdAt}
            poster={getPosterName(post.posterId)}
            text={post.text}
          />
        );
      })}
    </ul>
  );
}
