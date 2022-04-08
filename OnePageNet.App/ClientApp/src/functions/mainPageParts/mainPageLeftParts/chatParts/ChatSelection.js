  import React, { useState, useEffect } from "react";
  import '../../../styles/Chat.css';

export function ChatSelection({selectCurrentGroupChat, groups}) {

  return (
    <ul className='chat-selection'>
      {groups !== "There are no such entities in the database." ? (groups?.map((x) => {
        return <button className ='chat-selection-btn' onClick={() => selectCurrentGroupChat(x)}>{x.name}</button>;
      })):<>talk to some bitches</>}
    </ul>
  );
}
