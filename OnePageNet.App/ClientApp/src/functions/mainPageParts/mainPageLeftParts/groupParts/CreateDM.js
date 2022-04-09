import React from 'react';
import { useState, useEffect } from 'react';
import '../../../styles/Chat.css'

export function CreateDM({onNewGroup}) {
    const [friends,setFriends] = useState();

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

    const handleClick = (event) =>{
      
      const createGroup = async () => {
        const urlGroups = `https://localhost:7231/api/Groups/create/${sessionStorage.getItem(
          "currentUserId"
        )}/${event.id}`;
        const Name = event.userName;
        const MediaUri = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";
        
        fetch(urlGroups, {
          method: "POST",
          mode: "cors",
          headers: {
            "Content-Type": "application/json",
            Accept: "application/json",
            "Access-Control-Allow-Origin": "*",
          },
          body: JSON.stringify({
            Name,
            MediaUri,
          }),
        })
          .then((data) => data.json())
          .then((data) => onNewGroup(data));
      };

      createGroup()      
    }

    return (
      <ul className="friends-list">
        {friends !== "You don't have any friends." ? (
          friends?.map((x) => {
            return (
              <button
                className="chat-selection-btn"
                key={x.id}
                onClick={() => handleClick(x)}
              >
                {x.userName}
              </button>
            );
          })
        ) : (
          <>
            <h1>You do not have any friends</h1>
          </>
        )}
      </ul>
    );
}