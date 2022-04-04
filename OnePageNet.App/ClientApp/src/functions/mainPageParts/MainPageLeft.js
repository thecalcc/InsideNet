import React from "react"
import "../styles/MainPage.css"
import "../../chat/Chat.js"
import { Chat } from "../../chat/Chat.js"
import { ChatSelection } from "../../chat/ChatSelection.js";

export function MainPageLeft() {
    return (
      <div className="main-page-left">
        
        <Chat />
        <ChatSelection />
      </div>
    );
}