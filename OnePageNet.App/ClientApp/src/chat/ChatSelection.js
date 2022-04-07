import e from "cors";
import React, { useState, useEffect } from "react";

export function ChatSelection({selectCurrentGroupChat}) {
  const [groups, setGroups] = useState();

  useEffect(() => {
    const fetchGroups = async () => {
      const urlGroups = `https://localhost:7231/api/UserGroups/get-all/${sessionStorage.getItem('currentUserId')}`;

      await fetch(urlGroups, {
        method: "GET",
        mode: "cors",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
          "Access-Control-Allow-Origin": "*",
        },
      })
        .then((data) => data.json())
        .then((data) => setGroups(data));
    };
    fetchGroups();
  }, []);
  
  return (
    <ul>
      {groups !== "There are no such entities in the database." ? (groups?.map((x) => {
        return <button onClick={() => selectCurrentGroupChat(x.id)}>{x.name}</button>;
      })):<>talk to some bitches</>}
    </ul>
  );
}
