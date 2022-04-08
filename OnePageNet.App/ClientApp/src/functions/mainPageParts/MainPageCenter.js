/* eslint-disable default-case */
import React from "react";
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
    <div>

      {(() => {
        switch (currentLayout.center) {
          case "timeline":
            return (
              <div>
                <button onClick={() => onLayoutChange("create", "center")}>
                  Create Post
                </button>
                <PostList onSelect={choosePost} users={users} rerenderpls = {currentLayout} posts={posts}/>
              </div>
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
    </div>
  );
}
