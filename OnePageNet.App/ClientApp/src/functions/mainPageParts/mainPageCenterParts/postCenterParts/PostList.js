import React, { useState, useEffect } from "react";
import { PostListItem } from "./PostListItem";
import "../../../styles/PostList.css"
export function PostList({ onSelect, users, rerenderpls, posts}) {

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
            poster={getPosterName(post.posterId)}
            isMyPost={false}
          />
          <div>
            <button onClick={() => onSelect(post)}>Comments</button>
          </div>
        </li>
        );
      })}
    </ul>
  );
}
