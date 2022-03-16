import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { AcceptInvite, PendingInvite, Friend, Blocked } from "./UserRelationConstants";
import {Dropdown, DropdownButton, ButtonGroup}  from 'react-bootstrap'
import "../styles/Users.css";

export function Users() {
  const badRes = "There are no userRelationEntities in the database.";
  const [users, setUsers] = useState();
  const [userRelations, setUserRelations] = useState();
  const currentUserId = sessionStorage.currentUserId;
  useEffect(() => {
    const fetchUsers = async () => {
      const url = "https://localhost:7231/api/Users/get-all";
      const res = await fetch(url, {
        method: "GET",
        mode: "cors",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
          "Access-Control-Allow-Origin": "*",
        },
      });
      const json = await res.json();
      setUsers(json);
    };
    fetchUsers();
  }, [setUsers]);

  useEffect(() => {
    const fetchUserRelation = async () => {
      const url = `https://localhost:7231/api/UserRelations/get-all/${currentUserId}`;
      const res = await fetch(url, {
        method: "GET",
        mode: "cors",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
          "Access-Control-Allow-Origin": "*",
        },
      });
      const json = await res.json();
      if (json === badRes) {
        setUserRelations(undefined);
      } else {
        setUserRelations(json);
      }
    };
    fetchUserRelation();
  }, [setUserRelations]);

  const handleNewRelation = async (targetUserId) => {
    // TODO Get currentUser and pass it to the post request
    const url = "https://localhost:7231/api/userrelations/create";

    const res = await fetch(url, {
      method: "POST",
      mode: "cors",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        "Access-Control-Allow-Origin": "*",
      },
      body: JSON.stringify({
        currentUserId,
        targetUserId
      }),
    });

    const jsonRes = await res.json();
  };

  const handleClick = async (targetUserId, command) => {
    const url = "https://localhost:7231/api/userrelations/update";

    const res = await fetch(url, {
      method: "POST",
      mode: "cors",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        "Access-Control-Allow-Origin": "*",
      },
      body: JSON.stringify({
        currentUserId,
        targetUserId,
        command,
      }),
    });
  };

  return (
    <div className="container">
      <h3 className="p-3 text-center">Users List</h3>
      <table className="table table-striped table-bordered">
        <thead>
          <tr>
            <th>Name</th>
            <th>Username</th>
            <th>Status</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {users &&
            users.map((targetUser) =>{
              let hasRelation = false;
              return targetUser.id !== currentUserId ? (
                <tr key={targetUser.id}>
                  <td>
                    {targetUser.firstName} {targetUser.lastName}
                  </td>
                  <td>{targetUser.userName}</td>
                  {userRelations &&
                    userRelations.map((userRelation) => {
                      if (targetUser.id === userRelation.targetUserId) {
                        return(
                        <>
                          <td>{userRelation.userRelationship}</td>
                          {(() => {
                            switch (userRelation.userRelationship){
                              case AcceptInvite:
                                hasRelation = true;
                                return (<DropdownButton key='Accept Invite' title='User Actions'>
                                        <Dropdown.Item as="button" onClick={() => handleClick(targetUser.id, 'Accept Invite')}>Accept Invite</Dropdown.Item>
                                        <Dropdown.Item as="button" onClick={() => handleClick(targetUser.id, 'Decline Invite')}>Decline Invite</Dropdown.Item>
                                        <Dropdown.Item as="button" onClick={() => handleClick(targetUser.id, 'Block')}> Block </Dropdown.Item>
                                      </DropdownButton>)
                              case PendingInvite:
                                  hasRelation = true;
                                  return (<DropdownButton
                                    key='Pending Invite'
                                    title='User Actions'
                                  >
                                    <Dropdown.Item as="button" onClick={() => handleClick(targetUser.id, 'Abandon Invite')}>Stop Invite</Dropdown.Item>
                                    <Dropdown.Item as="button" onClick={() => handleClick(targetUser.id, 'Block')}> Block </Dropdown.Item>
                                  </DropdownButton>)
                              case Friend:
                                hasRelation = true;
                                return(
                                  <DropdownButton
                                  key='Friend'
                                  title='User Actions'
                                  >
                                <Dropdown.Item as="button" onClick={() => handleClick(targetUser.id, 'Unfriend')}>Unfriend</Dropdown.Item>
                                <Dropdown.Item as="button" onClick={() => handleClick(targetUser.id, 'Block')}> Block </Dropdown.Item>
                              </DropdownButton>
                                  )
                                default:
                                  return null
                            }
                          })()}
                        </>
                        )
                      }
                    })}
                    {
                      hasRelation != true ?
                      (
                        <>
                    <td>None</td>
                    <td>
                      <Link tag={Link} onClick={() => handleNewRelation(targetUser.id)}>
                        Send Friend Request
                      </Link>
                    </td>
                        </>
                      ):null
                    }
                    </tr>
              ) : (
                <></>
              )
            })}
        </tbody>
      </table>
    </div>
  );
}
