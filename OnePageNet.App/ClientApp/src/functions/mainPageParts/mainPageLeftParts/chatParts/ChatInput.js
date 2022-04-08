import React, { useState } from "react";
import '../../../../custom.css'
import '../../../styles/Chat.css'


export default function ChatInput({ sendMessage, onBack }) {
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
    <form className='chat-form' onSubmit={onSubmit}>
      <div className='chat-input'>
        <input
          className='text-input'
          type="text"
          id="message"
          name="message"
          value={message}
          placeholder='Message'
          onChange={onMessageUpdate}
        />
        <button className='custom-btn' type='submit'>
          <img className='btn-img' src='/resources/post-icon.png' alt='post-icon' />
        </button>
        <button className = 'custom-btn' onClick={() => onBack()}>
          <img className='btn-img' src='/resources/back-arrow-icon.png' alt='back-arrow-icon' />
        </button>
      </div>
    </form>
  );
};