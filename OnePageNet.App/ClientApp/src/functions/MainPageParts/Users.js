import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { AcceptInvite, PendingInvite, Friend } from "./UserRelationConstants";
export function Users() {
  const [users, setUsers] = useState();
  const [userRelations, setUserRelations] = useState();
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

  const handleClick = async (currentUserId, targetUserId, command) => {
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

    const jsonRes = await res.json();
  };

  useEffect(() => {
    const fetchUserRelation = async () => {
      const url =
        //TODO change for this to be the current user not a hard coded value
        `https://localhost:7231/api/UserRelations/get-all/${sessionStorage.currentUserId}`;
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
      setUserRelations(json);
    };
    fetchUserRelation();
  }, [setUserRelations]);
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
            users.map((user) =>
              user.id !== sessionStorage.currentUserId ? (
                <tr key={user.id}>
                  <td>
                    {user.firstName} {user.lastName}
                  </td>
                  <td>{user.userName}</td>
                  {userRelations &&
                    userRelations.map((userRelation) => {
                      if (user.id === userRelation.targetUser.id) {
                        if (userRelation.userRelationship === PendingInvite) {
                          return (
                            <>
                              <td> {userRelation.userRelationship} </td>
                              <td>
                                <Link
                                  tag={Link}
                                  onClick={() =>
                                    handleClick(
                                      user.id,
                                      userRelation.targetUser.id,
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
                                <Link tag={Link} value={AcceptInvite}>
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
                                <Link tag={Link} value={Friend}>
                                  Unfriend
                                </Link>
                              </td>
                            </>
                          );
                        }
                      }
                      return null;
                    })}
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
