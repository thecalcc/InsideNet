import { data } from "jquery";
import React, { useState, useEffect } from "react";
import "../styles/MainPage.css";
import { PostList } from "./mainPageCenterParts/postCenterParts/PostList";
import { CreatePost } from "./mainPageRightParts/postRightParts/CreatePost";

export function MainPageCenter({
  onLayoutChange,
  selectPost,
  currentLayout,
  users,
  posts
}) {
  
  const choosePost = (post) => {
    selectPost(post);
    onLayoutChange("post", "right");
  };
    

  return (
    <>

      {(() => {
        switch (currentLayout.center) {
          case "timeline":
            return (
              <ul>
                <button onClick={() => onLayoutChange("create", "center")}>
                  Create Post
                </button>
                <PostList onSelect={choosePost} users={users} rerenderpls = {currentLayout} posts={posts}/>
              </ul>
            );
          case "create":
            return (
              <>
                <CreatePost onLayoutChange={onLayoutChange}/>
                
                <button onClick={() => onLayoutChange("timeline", "center")}>
                  Back
                </button>
              </>
            );
        }
      })()}
    </>
  );
}
