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
}) {
  const [connection, setConnection] = useState(null);
  const latestChat = useRef(null);

  latestChat.current = chatHistory;

  const onEdit = () => {
    onLayoutChange("chat-edit", "left");
  };
  const onEditCompleation = () => {
    onLayoutChange("chat-display", "left");
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
        .then(() => {
          connection.on("ReceiveMessage", (message) => {
            const updatedChat = [...chatHistory.current, message];
            // latestChat.current = [...chatHistory.current, message];

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
      await connection
        .send("SendMessage", chatMessage)
        .then(() => onRerender());
      connection.on("ReceiveMessage", (message) => {
        const updatedChat = [...chatHistory.current, message];
        // latestChat.current = [...chatHistory.current, message];
        updatedChat.push(message);
        onChangeChatHistory(updatedChat);
      });
    } catch (e) {
      console.log("error");
      console.log(e);
    }
  };

  const leaveGroup = async () => {
    const url = `https://localhost:7231/api/UserGroups/delete/${group.id}/${sessionStorage.currentUserId}`;
    await fetch(url, {
      method: "DELETE",
      mode: "cors",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        "Access-Control-Allow-Origin": "*",
      },
    });
  };

  return (
    <div className="chat">
      {(() => {
        switch (layoutState) {
          case "chat-display":
            return (
              <div>
                {group.name}
                <button onClick={() => onEdit()}>rename</button>
                <button className="custom-btn" onClick={() => leaveGroup()}>
                  <img
                    className="btn-img"
                    src="/resources/delete-icon.png"
                    alt="delete-icon"
                  />
                </button>
              </div>
            );
          case "chat-edit":
            return (
              <EditGroup
                onRerender={onRerender}
                group={group}
                onEditCompleation={onEditCompleation}
              />
            );
        }
      })()}
      <ChatWindow chat={chatHistory} group={group} />
      <ChatInput sendMessage={sendMessage} onBack={onBack} />
    </div>
  );
}
