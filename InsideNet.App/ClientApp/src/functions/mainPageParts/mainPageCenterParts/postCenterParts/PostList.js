import React from "react";
import { PostListItem } from "./PostListItem";
import "../../../../custom.css";

export function PostList({ onSelect, users, posts, onLayoutChange, selectUser }) {
  const getPoster = (posterId) => {
    return users?.find((user) => user.id === posterId);
  };

  return (
    <ul className="post-list">
      {posts !== undefined
        ? posts?.map((post) => {
          return (
            <li key={post.id} className="post">
              <PostListItem
                post={post}
                poster={getPoster(post.posterId)}
                isMyPost={false}
                onLayoutChange = {onLayoutChange}
                selectUser={selectUser}
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
