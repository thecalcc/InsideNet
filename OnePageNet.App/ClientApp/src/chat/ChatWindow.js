import React, { useState } from "react";
import { Message } from "./Message";

export default function ChatWindow({ chat }) {
  return (
    <div>
      {chat.map((m) => (
        <Message key={Date.now() * Math.random()} message={m} />
      ))}
    </div>
  );
}
