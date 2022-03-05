import React, { useEffect, useState } from "react";

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
            })
            const json = await res.json();
            setUsers(json);
        };
        fetchUsers();
    }, [setUsers]);

    useEffect(() => {
        const fetchUserRelation = async () => {
            const url = "https://localhost:7231/api/UserRelations/get-all/f7206a4d-8eec-486a-9942-753d16b9e077";
            const res = await fetch(url, {
                method: "GET",
                mode: "cors",
                headers: {
                    "Content-Type": "application/json",
                    Accept: "application/json",
                    "Access-Control-Allow-Origin": "*",
                },
            })
            const json = await res.json();
            setUserRelations(json);
        };
        fetchUserRelation();
    }, [setUserRelations]);
    return (
        <div className="container">
            <h3 className="p-3 text-center">React - Display a list of items</h3>
            <table className="table table-striped table-bordered">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Username</th>
                        <th>Status</th>
                    </tr>
                </thead>
                <tbody>
                    {users && users.map(user => user.email != sessionStorage.email ? (
                        <>
                            <tr key={user.id}>
                                <td>{user.firstName} {user.lastName}</td>
                                <td>{user.userName}</td>
                                {/* <td>{userRelations.Id}</td>  */}
                            </tr>
                            {userRelations && userRelations.map(userRelation => userRelation
                            < tr key = { userRelations.Id } >
                                <td> {userRelation.Id} </td>
                            </tr>
                    )}
                        </>
                    ) :
                        <>
                        </>
                    )}
                    
            </tbody>
        </table>
        </div >
    );
}