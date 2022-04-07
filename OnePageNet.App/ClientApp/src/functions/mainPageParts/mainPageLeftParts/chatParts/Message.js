import React from 'react';

export function Message(props) {
  return (
    <div style={{ background: "#eee", borderRadius: "5px", padding: "0 10px" }}>
      <p>
        <strong>{props.user}</strong>says: {props.message.content}
      </p>
    </div>
  );
};