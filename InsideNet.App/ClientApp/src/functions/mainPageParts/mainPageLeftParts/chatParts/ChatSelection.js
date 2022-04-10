import React from "react";
import "../../../styles/Chat.css";

export function ChatSelection({ selectCurrentGroupChat, groups}) {
  
  return (
    <ul className="chat-selection">
      {groups !== "There are no such entities in the database." &&
      groups !== undefined ? (
        groups?.map((x) => {if(x.id !== undefined)
          return (
            <button
              key={x.id}
              className="chat-selection-btn"
              onClick={() => selectCurrentGroupChat(x)}
            >
              {x.name}
            </button>
          );
        })
      ) : (
        <>
          <h1>You are not in any groups</h1>
        </>
      )}
    </ul>
  );
}
