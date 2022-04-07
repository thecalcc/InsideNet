import e from "cors";
import React, { useState, useEffect } from "react";

export function ChatSelection({selectCurrentGroupChat, groups}) {

  
  return (
    <ul>
      {groups !== "There are no such entities in the database." ? (groups?.map((x) => {
        return <button onClick={() => selectCurrentGroupChat(x.id)}>{x.name}</button>;
      })):<>talk to some bitches</>}
    </ul>
  );
}
