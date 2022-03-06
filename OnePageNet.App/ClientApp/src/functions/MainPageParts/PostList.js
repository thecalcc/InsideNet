import React, { useState, useEffect } from "react"
import { PostListItem } from "./PostListItem"

export function PostList() {
    const [users, setUsers] = useState();
    const [posts, setPosts] = useState();

    useEffect(async () => {
        const urlPosts = "https://localhost:7231/api/Posts/get-all";
        await fetch(urlPosts, {
            method: "GET",
            mode: "cors",
            headers: {
                "Content-Type": "application/json",
                Accept: "application/json",
                "Access-Control-Allow-Origin": "*",
            },
        }).then(data => data.json()).then(data => setPosts(data));

        const urlUsers = "https://localhost:7231/api/Users/get-all";
        await fetch(urlUsers, {
            method: "GET",
            mode: "cors",
            headers: {
                "Content-Type": "application/json",
                Accept: "application/json",
                "Access-Control-Allow-Origin": "*",
            },
        }).then(data => data.json()).then(data => setUsers(data));



    }, [setPosts, setUsers]);

    const getPosterName = (posterId) => {
        return users.map((user) =>
            posterId == user.Id ? user.userName : null
        )
    }

    return (
        <ul>
            {posts?.map((post) => {
                const posterName = getPosterName(post.posterId);
                <PostListItem createdAt={post.createdAt} poster={posterName} text={post.text} />
            }
            )}
        </ul>
    )
}