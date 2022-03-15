import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { AcceptInvite, PendingInvite, Friend } from "./UserRelationConstants";
export function Users() {
  const badRes = "There are no userRelationEntities in the database.";
  const [users, setUsers] = useState();
  const [userRelations, setUserRelations] = useState();
  const currentUserId = sessionStorage.currentUserId;
  const [currentUser, setCurrentUser] = useState();
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

  const handleNewRelation = async (targetUser) => {
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
        currentUser,
        targetUser,
        PendingInvite
      }),
    });

    const jsonRes = await res.json();
  };

  const handleClick = async (currentUser, targetUser, userRelationship) => {
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
        currentUser,
        targetUser,
        userRelationship,
      }),
    });

    const jsonRes = await res.json();
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
            users.map((targetUser) =>
              targetUser.id !== currentUserId ? (
                <tr key={targetUser.id}>
                  <td>
                    {targetUser.firstName} {targetUser.lastName}
                  </td>
                  <td>{targetUser.userName}</td>
                  {userRelations &&
                    userRelations.map((userRelation) => {
                      if (targetUser.id === userRelation.targetUser.id) {
                        if (userRelation.userRelationship === PendingInvite) {
                          return (
                            <>
                              <td> {userRelation.userRelationship} </td>
                              <td>
                                <Link
                                  tag={Link}
                                  onClick={() =>
                                    handleClick(
                                      userRelation.currentUser,
                                      userRelation.targetUser,
                                      PendingInvite
                                    )
                                  }
                                >
                                  Reject Invite
                                </Link>
                              </td>
                            </>
                          );
                        } else if (
                          userRelation.userRelationship === AcceptInvite
                        ) {
                          return (
                            <>
                              <td> {userRelation.userRelationship} </td>
                              <td>
                                <Link
                                  tag={Link}
                                  onClick={() =>
                                    handleClick(
                                      userRelation.currentUser,
                                      userRelation.targetUser,
                                      AcceptInvite
                                    )
                                  }
                                >
                                  Accept Invite
                                </Link>
                              </td>
                            </>
                          );
                        } else {
                          return (
                            <>
                              <td> {userRelation.userRelationship} </td>
                              <td>
                                <Link
                                  tag={Link}
                                  onClick={() =>
                                    handleClick(
                                      userRelation.currentUser,
                                      userRelation.targetUser,
                                      Friend
                                    )
                                  }
                                >
                                  Unfriend
                                </Link>
                              </td>
                            </>
                          );
                        }
                      }
                      return null;
                    })}
                  <td></td>
                  <td>
                    <Link
                      tag={Link}
                      onClick={() => handleNewRelation(targetUser)}
                    >
                      Send Friend Request
                    </Link>
                  </td>
                </tr>
              ) : (
                <></>
              )
            )}
        </tbody>
      </table>
    </div>
  );
}
