import React, { useState, useEffect } from "react";
import { PostListItem } from "./PostListItem";

export function PostList() {
  const [users, setUsers] = useState();
  const [posts, setPosts] = useState();

  useEffect(() => {
    async function fetchPosts() {
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
    }

    async function fetchUsers() {
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
    }

    fetchPosts();
    fetchUsers();
  });

  const getPosterName = (posterId) => {
    return users.map((user) => {
      if (posterId === user.Id) {
        return user.userName;
      } else {
        return null;
      }
    });
  };

  return (
    <ul>
      {posts?.map((post) => {
        return (
          <PostListItem
            createdAt={post.createdAt}
            poster={(x) => getPosterName(x.posterId)}
            text={post.text}
          />
        );
      })}
    </ul>
  );
}
