/* eslint-disable default-case */
import React, { useState, useEffect, useRef } from "react";
import * as signalR from "@microsoft/signalr";
import ChatWindow from "./ChatWindow";
import ChatInput from "./ChatInput";
import "../../../styles/Chat.css";
import { EditGroup } from "../groupParts/EditGroup";

export function Chat({
  group,
  onBack,
  layoutState,
  onLayoutChange,
  onRerender,
  chatHistory,
  onChangeChatHistory,
  onChangeGroup
}) {
  const [connection, setConnection] = useState(null);
  const latestChat = useRef(null);

  latestChat.current = chatHistory;

  const onEdit = () => {
    onLayoutChange("chat-edit", "left");
  };
  useEffect(() => {
    const options = {
      logger: signalR.LogLevel.Trace,
      "Access-Control-Allow-Origin": "*",
    };

    const newConnection = new signalR.HubConnectionBuilder()
      .withUrl("https://localhost:7231/hubs/chat", { headers: { ...options } })
      .withAutomaticReconnect()
      .build();

    setConnection(newConnection);
  }, []);

  useEffect(() => {
    if (connection) {
      connection
        .start()
        .then((result) => {
          console.log("Connected!");

          connection.on("ReceiveMessage", (message) => {
            const updatedChat = [...chatHistory.current];

            updatedChat.push(message);
            onChangeChatHistory(updatedChat);
          });
        })
        .catch((e) => console.log("Connection failed: ", e));
    }
  }, [connection]);

  const sendMessage = async (message) => {
    const chatMessage = {
      senderId: sessionStorage.getItem("currentUserId"),
      content: message,
      destinationId: group.id,
    };

    try {
      await connection.send("SendMessage", chatMessage);
      onRerender();
      const updatedChat = [...chatHistory];
      console.log(...chatHistory);
      updatedChat.push(message);
      onChangeChatHistory(updatedChat);
    } catch (e) {
      console.log("error");
      console.log(e);
    }
  };

  const leaveGroup = async () =>{
    const url = `https://localhost:7231/api/UserGroups/delete/${group.id}/${sessionStorage.currentUserId}`;
    fetch(url, {
        method: "DELETE",
        mode: "cors",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
          "Access-Control-Allow-Origin": "*",
        },
      }).then(() => onChangeGroup(null));

  }

  return (
    <div className="chat">
      {(() => {
        switch (layoutState) {
          case "chat-display":
            return (
              <div className = 'chat-settings'>
                <div className = 'chat-name'>{group.name}</div>
                <button className = 'custom-btn' onClick={() => onEdit()}><img className = 'btn-img' src = '/resources/edit-icon.png' alt = 'edit-icon'></img></button>
                <button className="custom-btn" onClick={() => leaveGroup()}><img className='btn-img' src='/resources/delete-icon.png' alt='delete-icon' /></button>
              </div>
            );
          case "chat-edit":
            return (
              <EditGroup
                group={group}
                onChangeGroup = {onChangeGroup}
              />
            );
        }
      })()}
      <ChatWindow chat={chatHistory} group = {group}/>
      <ChatInput sendMessage={sendMessage} onBack={onBack} />
    </div>
  );
}