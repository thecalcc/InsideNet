/* eslint-disable default-case */
import React from "react";
import "../styles/MainPage.css";
import { PostList } from "./mainPageCenterParts/postCenterParts/PostList";
import { CreatePost } from "./mainPageRightParts/postRightParts/CreatePost";
import { UserSettings } from "../../functions/mainPageParts/mainPageCenterParts/userSettingsParts/UserSettings";

export function MainPageCenter({
  onLayoutChange,
  selectPost,
  layoutState,
  users,
  posts,
}) {
  const choosePost = (post) => {
    selectPost(post);
    onLayoutChange("post", "right");
  };

  return (
    <div>
      {(() => {
        switch (layoutState) {
          case "timeline":
            return (
              <div>
                <button onClick={() => onLayoutChange("create", "center")}>
                  Create Post
                </button>
                <PostList
                  onSelect={choosePost}
                  users={users}
                  rerenderpls={layoutState}
                  posts={posts}
                />
              </div>
            );
          case "create":
            return (
              <>
                <CreatePost onLayoutChange={onLayoutChange} />

                <button onClick={() => onLayoutChange("timeline", "center")}>
                  Back
                </button>
              </>
            );
          case "settings-personal-info":
          case "settings-account":
          case "settings-about":
            return (
              <>
                <UserSettings
                  onLayoutChange={onLayoutChange}
                  layoutState={layoutState}
                />
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
