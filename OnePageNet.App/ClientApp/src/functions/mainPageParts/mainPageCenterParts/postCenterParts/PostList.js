import React, { useState, useEffect } from "react";
import { PostListItem } from "./PostListItem";
import "../../../../custom.css";

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
            <button className = "custom-btn" onClick={() => onSelect(post)}><img className='btn-img' src= '/resources/comment-icon.png' alt='comment-icon'/> </button>
          </div>
        </li>
        );
      })}
    </ul>
  );
}
