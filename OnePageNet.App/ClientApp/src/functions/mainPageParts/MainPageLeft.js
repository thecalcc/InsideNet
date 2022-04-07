import React from "react";
import "../styles/MainPage.css";
import "../../chat/Chat.js";
import { Chat } from "../../chat/Chat.js";
import { ChatSelection } from "../../chat/ChatSelection.js";
import { useState } from "react";
import { CreateDM } from "../../chat/CreateDM";

export function MainPageLeft() {
  const [currentGroup, setCurrentGroup] = useState(null);

  const selectCurrentGroupChat = (prop) => {
    setCurrentGroup(prop);
  };

  return (
    <div className="main-page-left">
      {currentGroup === null || currentGroup === undefined ? (
        <>
          <CreateDM />
          <ChatSelection selectCurrentGroupChat={selectCurrentGroupChat} />
        </>
      ) : (
        <>
          <Chat group={currentGroup} />
          <button onClick={() => setCurrentGroup(null)}>Back</button>
        </>
      )}
    </div>
  );
}
