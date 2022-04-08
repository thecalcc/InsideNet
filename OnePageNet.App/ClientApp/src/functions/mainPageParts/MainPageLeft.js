import React from "react";
import "../styles/MainPage.css";
import { Chat } from "./mainPageLeftParts/chatParts/Chat";
import { ChatSelection } from "./mainPageLeftParts/chatParts/ChatSelection.js";
import { useState, useEffect } from "react";
import { CreateDM } from "./mainPageLeftParts/groupParts/CreateDM";

export function MainPageLeft({ layoutState, onLayoutChange }) {
  const [currentGroup, setCurrentGroup] = useState(null);
  const [rerender, setRerender] = useState(false);
  const onRerender = () => {
    setRerender(!rerender);
  };

  const selectCurrentGroupChat = (prop) => {
    setCurrentGroup(prop);
    onLayoutChange("chat", "left");
  };

  const onBack = () => {
    onLayoutChange("groupSelection", "left")
  }

  const [groups, setGroups] = useState();

  useEffect(() => {
    const fetchGroups = async () => {
      const urlGroups = `https://localhost:7231/api/UserGroups/get-all/${sessionStorage.getItem(
        "currentUserId"
      )}`;

      await fetch(urlGroups, {
        method: "GET",
        mode: "cors",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
          "Access-Control-Allow-Origin": "*",
        },
      })
        .then((data) => data.json())
        .then((data) => setGroups(data));
    };
    fetchGroups();
  }, [rerender]);

  return (
    <div className="main-page-left">
      {(() => {
        switch (layoutState) {
          case "groupSelection":
            return (
              <>
                <CreateDM onRerender={onRerender}/>
                <ChatSelection
                  selectCurrentGroupChat={selectCurrentGroupChat}
                  groups={groups}
                />
              </>
            );
          case "chat":
            return (
              <>
                <Chat group={currentGroup} onBack = {onBack} />
              </>
            );
        }
      })()}
    </div>
  );
}
