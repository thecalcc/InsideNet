/* eslint-disable react-hooks/exhaustive-deps */
/* eslint-disable default-case */
import React from "react";
import "../styles/MainPage.css";
import { Chat } from "./mainPageLeftParts/chatParts/Chat";
import { ChatSelection } from "./mainPageLeftParts/chatParts/ChatSelection.js";
import { useState, useEffect } from "react";
import { CreateDM } from "./mainPageLeftParts/groupParts/CreateDM";

export function MainPageLeft({ layoutState, onLayoutChange }) {
  const [currentGroup, setCurrentGroup] = useState(null);
  const [rerender, setRerender] = useState(false);
  const [chatHistory, setChatHistory] = useState([]);
  const [groups, setGroups] = useState();

  const onChangeChatHistory = (history) => {
    setChatHistory(history);
  };

  const onRerender = () => {
    setRerender(!rerender);
  };
  const onNewGroup = (group) =>{
    if(group !== 'This group already exists.')
    setGroups([...groups, group]);
  }
  const selectCurrentGroupChat = (prop) => {
    setCurrentGroup(prop);
    onLayoutChange("chat-display", "left");
  };

  const onBack = () => {
    onLayoutChange("groupSelection", "left");
  };

  const onChangeGroup = (group) => {
    if(group !== null) onLayoutChange("chat-display", "left");
    else onBack()
    setCurrentGroup(group);
  }

  useEffect(() => {
    const fetchUserGroups = async () => {
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

    const getChatHistory = async () => {
      const url = `https://localhost:7231/api/messages/get-history/${currentGroup.id}`;

      await fetch(url, {
        method: "GET",
        mode: "cors",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
          "Access-Control-Allow-Origin": "*",
        },
      })
        .then((data) => data.json())
        .then((data) => setChatHistory(data));
    };

    if (currentGroup?.id !== undefined) {
      getChatHistory();
    }
    fetchUserGroups();
  }, [rerender, currentGroup]);

  return (
    <div className="main-page-left">
      {(() => {
        switch (layoutState) {
          case "groupSelection":
            return (
              <>
                <CreateDM onNewGroup={onNewGroup}/>
                <ChatSelection
                  selectCurrentGroupChat={selectCurrentGroupChat}
                  groups={groups}
                />
              </>
            );
          case "chat-display":
          case "chat-edit":
            return (
              <>
                <Chat
                  group={currentGroup}
                  onBack={onBack}
                  layoutState={layoutState}
                  onLayoutChange={onLayoutChange}
                  onRerender={onRerender}
                  chatHistory={chatHistory}
                  onChangeChatHistory={onChangeChatHistory}
                  onChangeGroup = {onChangeGroup}
                />
              </>
            );
        }
      })()}
    </div>
  );
}