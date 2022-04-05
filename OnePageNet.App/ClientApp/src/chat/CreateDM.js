import { grep } from 'jquery';
import React from 'react';
import { useState, useEffect } from 'react';
import { canConstructReadableStream } from 'workbox-core/_private';

export function CreateDM() {
    const [friends,setFriends] = useState();
    const [group, setGroup] = useState();
    const [secondUserId, setSecondUserId] =  useState();

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

    useEffect(() => {
      const createUserGroup = async (usersId, groupId) => {
        const urlFriends = `https://localhost:7231/api/UserGroups/create`;
        await fetch(urlFriends, {
          method: "POST",
          mode: "cors",
          headers: {
            "Content-Type": "application/json",
            Accept: "application/json",
            "Access-Control-Allow-Origin": "*",
          },
          body: JSON.stringify({
            groupId,
            usersId
          }),
        })
      };
      const setUserGroups = (group) => {
        createUserGroup(secondUserId, group.id)
        createUserGroup(sessionStorage.getItem("currentUserId"), group.id);
      }
      if(group != null){setUserGroups(group)};

    },[group])

    const handleClick = (event) =>{
      
      setSecondUserId(event.id);
      const createGroup = async () => {
        const urlGroups = `https://localhost:7231/api/Groups/create`;
        const Name = event.userName;
        const MediaUri = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";
        await fetch(urlGroups, {
          method: "POST",
          mode: "cors",
          headers: {
            "Content-Type": "application/json",
            Accept: "application/json",
            "Access-Control-Allow-Origin": "*",
          },
          body: JSON.stringify({
            Name,
            MediaUri
          }),
        })
          .then((data) => data.json())
          .then((data) => setGroup(data));
          
      };

      

      createGroup()
      
    }

    return (
      <ul>
        {friends != "You don't have any friends." ? (
          friends?.map((x) => {
            return <button onClick={() => handleClick(x)}>{x.userName}</button>;
          })
        ) : (
          <>get some bitches</>
        )}
      </ul>
    );
}