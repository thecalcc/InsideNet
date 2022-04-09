import React from 'react';
import '../../../styles/Chat.css'
  export function Message(props) {


  return (
    <div className = 'msg-internal'>
      <div className='msg-username'>
        {props.userName}
      </div>
      {props.message.content}
    </div>
  );
};