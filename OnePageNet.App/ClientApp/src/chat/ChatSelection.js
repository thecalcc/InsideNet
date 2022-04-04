import React, { useState, useEffect } from "react";

export function ChatSelection() {
  const [freinds, setFriends] = useState();

  useEffect(() => {
    const fetchFriends = async () => {
      const urlFriends = `https://localhost:7231/api/Users/get-all-friends/${sessionStorage.getItem(
        "currentUserId"
      )}`;

      await fetch(urlFriends, {
        method: "GET",
        mode: "cors",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
          "Access-Control-Allow-Origin": "*",
        },
      })
        .then((data) => data.json())
        .then((data) => setFriends(data));
    };
    fetchFriends();
  }, []);

  return (
    <ul>
      {freinds?.map((x) => {
        return <button onClick={""}>{x.userName}</button>;
      })}
    </ul>
  );
}
