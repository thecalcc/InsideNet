/* eslint-disable default-case */
import React, { useState } from "react";
import { PostList } from "./mainPageCenterParts/postCenterParts/PostList";
import { CreatePost } from "./mainPageRightParts/postRightParts/CreatePost";
import { UserSettings } from "../../functions/mainPageParts/mainPageCenterParts/userSettingsParts/UserSettings";
import { useEffect } from "react";

import "../styles/MainPage.css";

export function MainPageCenter({
  onLayoutChange,
  selectPost,
  layoutState,
  users,
  posts,
}) {
  const [user, setUser] = useState();
  const [accountSettings, setAccountSettings] = useState();

  const choosePost = (post) => {
    selectPost(post);
    onLayoutChange("post", "right");
  };

  useEffect(() => {
    const fetchAccountSettings = async () => {
      const url = `https://localhost:7231/api/AccountSettings/get/${sessionStorage.currentUserId}`;

      await fetch(url, {
        method: "GET",
        mode: "cors",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
          "Access-Control-Allow-Origin": "*",
        },
      })
        .then((x) => x.json())
        .then((x) => setAccountSettings(x));
    };
    fetchAccountSettings();

    const fetchUser = async () => {
      const url = `https://localhost:7231/api/users/get/${sessionStorage.currentUserId}`;

      await fetch(url, {
        method: "GET",
        mode: "cors",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
          "Access-Control-Allow-Origin": "*",
        },
      })
        .then((x) => x.json())
        .then((x) => setUser(x));
    };
    fetchUser();
  }, [layoutState]);
    
    return (
    <div className='main-page-center'>
      {(() => {
        switch (layoutState) {
          case "timeline":
            return (
              <div className='main-page-center-posts'>
                <CreatePost onLayoutChange={onLayoutChange}/>
                <PostList onSelect={choosePost} users={users} posts={posts} />
              </div>
            );
          case "settings-personal-info-edit":
          case "settings-account-edit":
          case "settings-contacts-edit":
          case "settings-personal-info":
          case "settings-account":
          case "settings-contacts":
            return (
              <>
                <UserSettings
                  onLayoutChange={onLayoutChange}
                  layoutState={layoutState}
                  user={user}
                  accountSettings={accountSettings}
                />
              </>
            );
        }
      })()}
    </div>
  );
}
