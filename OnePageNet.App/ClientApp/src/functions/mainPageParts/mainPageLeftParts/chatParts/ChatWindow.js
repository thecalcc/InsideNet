import React from "react";
import { Message } from "./Message";
import '../../../styles/Chat.css';


export default function ChatWindow({ chat, group }) {
  return (
    <>
      <div>
        {group.name}
      </div>
      <ul className='chat-window'>
        {chat.map((m) => (
          <div className='msg'>
            {(() => {
              if (m.senderId == sessionStorage.getItem('currentUserId')) {
                return <div className='msg-self'>
                  <Message key={Date.now() * Math.random()} message={m} />
                </div>
              } else {
                return <div className='msg-other'>
                  <Message key={Date.now() * Math.random()} message={m} />
                </div>
              }
            })()
            }
          </div>
        ))}
      </ul>
    </>
  );
}
