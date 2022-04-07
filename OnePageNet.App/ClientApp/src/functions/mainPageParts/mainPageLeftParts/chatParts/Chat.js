import React, { useState, useEffect, useRef } from "react";
import * as signalR from "@microsoft/signalr";

import ChatWindow from "./ChatWindow";
import ChatInput from "./ChatInput";

export function Chat({ group }) {
  const [connection, setConnection] = useState(null);
  const latestChat = useRef(null);
  const [rerender, setRerender] = useState(false);
  const [chatHistory, setChatHistory] = useState([]);

  latestChat.current = chatHistory;

  useEffect(() => {
    const getChatHistory = async () => {
      const url = `https://localhost:7231/api/messages/get-history/${group}`;

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
    getChatHistory();
  }, [rerender]);

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
            setChatHistory(updatedChat);
          });
        })
        .catch((e) => console.log("Connection failed: ", e));
    }
  }, [connection]);

  const sendMessage = async (message) => {
    const chatMessage = {
      senderId: sessionStorage.getItem("currentUserId"),
      content: message,
      destinationId: group,
    };

    try {
      await connection.send("SendMessage", chatMessage);
      setRerender(!rerender);      
      updatedChat.push(message);
      const updatedChat = [...chatHistory.current];
      setChatHistory(updatedChat);
      
    } catch (e) {
      console.log("error");
      console.log(e);
    }
  };

  return (
    <div>
      <ChatInput sendMessage={sendMessage} />
      <hr />
      <ChatWindow chat={chatHistory} />
    </div>
  );
}
