import React, { useState, useEffect, useRef } from "react";
import * as signalR from "@microsoft/signalr";

import ChatWindow from "./ChatWindow";
import ChatInput from "./ChatInput";
import ChatSelection from "./ChatSelection";

export function Chat() {
  const [connection, setConnection] = useState(null);
  const [chat, setChat] = useState([]);
  const latestChat = useRef(null);

  latestChat.current = chat;

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
            const updatedChat = [...latestChat.current];
            updatedChat.push(message);

            setChat(updatedChat);
          });
        })
        .catch((e) => console.log("Connection failed: ", e));
    }
  }, [connection]);

  const sendMessage = async (message) => {
    const chatMessage = {
      senderId: sessionStorage.getItem('currentUserId'),
      content: message,
    };

      try {
        await connection.send("SendMessage", chatMessage);
      } catch (e) {
          console.log('error')
        console.log(e);
      }
  };

  return (
    <div>
      <ChatInput sendMessage={sendMessage} />
      <hr />
      <ChatWindow chat={chat} />
    </div>
  );
};
