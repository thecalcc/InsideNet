import React, { useEffect, useState } from "react";
import { AcceptInvite, PendingInvite, Friend } from "./UserRelationConstants";
import { Dropdown, DropdownButton } from 'react-bootstrap'
import "../../../styles/Users.css";
import { DropdownItem, DropdownMenu } from "reactstrap";

export function Users() {
  const badRes = "There are no userRelationEntities in the database.";
  const [users, setUsers] = useState();
  const [userRelations, setUserRelations] = useState();
  const [search, setSearch] = useState();
  const currentUserId = sessionStorage.currentUserId;

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
    if (json === badRes) {
      setUsers(undefined);
    } else {
      setUsers(json);
    }
  };

  useEffect(() => {
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

  const handleSubmit = async (e) => {
    e.preventDefault();
    const fetchFilteredUsers = async () => {
      const url = `https://localhost:7231/api/Users/get-filtered-users/${search}`;
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
        setUsers(undefined);
      } else {
        setUsers(json);
      }
    };
    if (search != "" && search != null && search != undefined) fetchFilteredUsers();
    else fetchUsers();
  }

  return (
    <div className = 'users'>
      <form className = 'search-form' onSubmit={(e) => handleSubmit(e)}>
        <input
          className='text-input'
          name="search"
          type="text"
          placeholder="Search for user here..."
          value={search}
          onChange={(e) => setSearch(e.target.value)}
        />
        <button className='custom-btn' type="submit"><img className = 'btn-img' src='/resources/search-icon.png' alt = 'search-icon'></img></button>
      </form>
      <div className='table-wrapper'>
        <div className='table-scroll'>
          <table className="table table-bordered">
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
                users.map((targetUser) => {
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
                            return (
                              <>
                                <td>{userRelation.userRelationship}</td>
                                {(() => {
                                  switch (userRelation.userRelationship) {
                                    case AcceptInvite:
                                      hasRelation = true;
                                      return (<Dropdown key='Accept Invite' >
                                        <Dropdown.Toggle id="dropdown-button-dark-example1" variant="warning">
                                          User Actions
                                        </Dropdown.Toggle>
                                        <Dropdown.Menu>
                                          <Dropdown.Item as="button" onClick={() => handleClick(targetUser.id, 'Accept Invite')}>Accept Invite</Dropdown.Item>
                                          <Dropdown.Item as="button" onClick={() => handleClick(targetUser.id, 'Decline Invite')}>Decline Invite</Dropdown.Item>
                                          <Dropdown.Item as="button" onClick={() => handleClick(targetUser.id, 'Block')}> Block </Dropdown.Item>
                                        </Dropdown.Menu>
                                      </Dropdown>)
                                    case PendingInvite:
                                      hasRelation = true;
                                      return (<Dropdown
                                        key='Pending Invite'
                                      >
                                        <Dropdown.Toggle id="dropdown-button-dark-example1" variant="warning">
                                          User Actions
                                        </Dropdown.Toggle>
                                        <Dropdown.Menu variant="dark">
                                          <Dropdown.Item as="button" onClick={() => handleClick(targetUser.id, 'Abandon Invite')}>Stop Invite</Dropdown.Item>
                                          <Dropdown.Item as="button" onClick={() => handleClick(targetUser.id, 'Block')}> Block </Dropdown.Item>
                                        </Dropdown.Menu>
                                      </Dropdown>)
                                    case Friend:
                                      hasRelation = true;
                                      return (
                                        <Dropdown
                                          key='Friend'
                                        >
                                          <Dropdown.Toggle id="dropdown-button-dark-example1" variant="warning">
                                            User Actions
                                          </Dropdown.Toggle>
                                          <Dropdown.Menu variant='dark'>
                                            <Dropdown.Item as="button" onClick={() => handleClick(targetUser.id, 'Unfriend')}>Unfriend</Dropdown.Item>
                                            <Dropdown.Item as="button" onClick={() => handleClick(targetUser.id, 'Block')}> Block </Dropdown.Item>
                                          </Dropdown.Menu>
                                        </Dropdown>
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
                                <Dropdown
                                  key='Friend'>
                                  <Dropdown.Toggle id="dropdown-button-dark-example1" variant="warning">
                                    User Actions
                                  </Dropdown.Toggle>
                                  <Dropdown.Menu variant = 'dark'>
                                    <Dropdown.Item as="button" onClick={() => handleNewRelation(targetUser.id)}>Add Friend</Dropdown.Item>
                                    <Dropdown.Item as="button" onClick={() => handleClick(targetUser.id, 'Block')}> Block </Dropdown.Item>
                                  </Dropdown.Menu>
                                </Dropdown>
                              </td>
                            </>
                          ) : null
                      }
                    </tr>
                  ) : (
                    <></>
                  )
                })}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
}
