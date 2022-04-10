import React, { useEffect, useState, useRef } from "react";
import { Message } from "./Message";
import '../../../styles/Chat.css';


export default function ChatWindow({ chat, group }) {
  const [currentUserUsername, setCurrentUserUsername] = useState();
  const [otherUserUsername, setOtherUserUsername] = useState();

  const messagesEndRef = useRef(null)

  const scrollToBottom = () => {
    messagesEndRef.current?.scrollIntoView({ behavior: "smooth" })
  }

  useEffect(() => {
    scrollToBottom();
  }, [chat]);

  useEffect(() => {
    const url = `https://localhost:7231/api/UserGroups/get-group-participants/${group.id}`;
    fetch(url, {
      method: "GET",
      mode: "cors",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        "Access-Control-Allow-Origin": "*",
      },
    })
    .then((data) => data.json())
    .then((data) => {
      const [currentUser] = data.filter(x => x.id === sessionStorage.getItem('currentUserId'));
      const [otherUser] = data.filter(x => x.id !== currentUser.id);
      if(currentUser !== undefined) setCurrentUserUsername(currentUser.userName);
      if(otherUser !== undefined) setOtherUserUsername(otherUser.userName);
    })
  }, [])

  return (
    <ul className="chat-window">
      {chat.map((m) => (
        <>
          <div key={Math.random()} className="msg">
            {(() => {
              if (m.senderId === sessionStorage.getItem("currentUserId")) {
                return (
                  <div className="msg-self">
                    <Message
                      key={Date.now() * Math.random()}
                      message={m}
                      userName={currentUserUsername}
                    />
                  </div>
                );
              } else {
                return (
                  <div className="msg-other">
                    <Message
                      key={Date.now() * Math.random()}
                      message={m}
                      userName={otherUserUsername}
                    />
                  </div>
                );
              }
            })()}
          </div>
          <div ref={messagesEndRef} />
        </>
      ))}
    </ul>
  );
}
