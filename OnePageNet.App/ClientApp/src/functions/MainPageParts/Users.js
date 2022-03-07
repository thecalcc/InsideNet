import React, { useEffect, useState } from "react";
import { AcceptInvite, PendingInvite, Friend } from "./UserRelationConstants"
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
                    userRelations.map((userRelation) =>
                      user.userName === userRelation.targetUser ? (
                        <td> {userRelation.userRelationship} </td>
                        // userRelation.userRelationship == PendingInvite ?
                        // <td><button type="button" value={PendingInvite}>Reject Invite</button></td> :
                        // <td><button type="button" value={AcceptInvite}>Accept Invite</button></td>
                    ) : (
                  <></>
                  )
                      )}
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
