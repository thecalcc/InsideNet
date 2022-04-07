import React, { useState } from "react";

export default function ChatInput({ sendMessage }) {
  const [message, setMessage] = useState("");

  const onSubmit = (e) => {
    e.preventDefault();

    if (message && message !== "") {
      sendMessage(message);
    } else {
      alert("Please insert a user and a message.");
    }
  };

  const onMessageUpdate = (e) => {
    setMessage(e.target.value);
  };

  return (
    <form onSubmit={onSubmit}>
      <label htmlFor="message">Message:</label>
      <br />
      <input
        type="text"
        id="message"
        name="message"
        value={message}
        onChange={onMessageUpdate}
      />
      <br />
      <br />
      <button>Send</button>
    </form>
  );
};