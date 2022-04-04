import React from 'react';
import { Message } from './Message';

export default function ChatWindow(props) {
  const chat = props.chat.map((m) => (
    <Message
      key={Date.now() * Math.random()}
      message={m}
    />
  ));

  return <div>{chat}</div>;
};