import React from "react";
import { PostListItem } from "./PostListItem";
import "../../../../custom.css";

export function PostList({ onSelect, users, rerenderpls, posts }) {
  const getPosterName = (posterId) => {
    return users?.find((user) => user.id === posterId).userName;
  };

  return (
    <ul className="post-list">
      {posts !== undefined
        ? posts?.map((post) => {
            return (
              <li key={post.id} className="post">
                <PostListItem
                  post={post}
                  poster={getPosterName(post.posterId)}
                  isMyPost={false}
                />
                <div>
                  <button className="custom-btn" onClick={() => onSelect(post)}>
                    <img
                      className="btn-img"
                      src="/resources/comment-icon.png"
                      alt="comment-icon"
                    />{" "}
                  </button>
                </div>
              </li>
            );
          })
        : null}
    </ul>
  );
}
