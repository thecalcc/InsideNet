import React, { useEffect, useState } from "react";

export function Users() {
    const [users, setUsers] = useState();

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
    return (
        <div className="container">
            <h3 className="p-3 text-center">React - Display a list of items</h3>
            <table className="table table-striped table-bordered">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Email</th>
                        <th>Username</th>
                    </tr>
                </thead>
                <tbody>
                    {users && users.map(user =>
                        <tr key={user.id}>
                            <td>{user.firstName} {user.lastName}</td>
                            <td>{user.email}</td>
                            <td>{user.userName}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>
    );
}