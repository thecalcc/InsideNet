import React, { useEffect, useState } from "react";
import dateFormat from "dateformat";
import "../../../styles/UserSettings.css";
import "../../../styles/PostList.css";
import { PostListItem } from "../../mainPageCenterParts/postCenterParts/PostListItem";



export function UserProfile({ user, post, onSelect, onLayoutChange }) {
    const [posts, setPosts] = useState();
    useEffect(() => {
        const urlPosts = `https://localhost:7231/api/Posts/get-all/${user.id}`;
        fetch(urlPosts, {
            method: "GET",
            mode: "cors",
            headers: {
                "Content-Type": "application/json",
                Accept: "application/json",
                "Access-Control-Allow-Origin": "*",
            },
        }).then(data => data.json())
            .then(data => setPosts(data));
    }, [])


    return (
        <div className = 'user-profile'>
            <div className='settings-small'>
                <ul className='settings-list'>
                    <li>Name: {user.firstName} {user.lastName}</li>
                    <li>Birth day: {dateFormat(user.doB, "dddd, mmmm dS, yyyy")}</li>
                    <li>Gender: {user.gender ? user.gender : 'unspecified'}</li>
                    <li>Email: {user.email}</li>
                    <li>Phone number: {user.phoneNumber ? user.phoneNumber : 'No phone number'}</li>
                </ul>
            </div>
            <ul className="post-list-small">
                {posts !== undefined
                    ? posts?.map((post) => {
                        return (
                            <li key={post.id} className="post">
                                <PostListItem
                                    post={post}
                                    poster={user}
                                    isMyPost={false}
                                />
                                <div>
                                    <button className="custom-btn" onClick={() => onSelect(post)}>
                                        <img
                                            className="btn-img"
                                            src="/resources/comment-icon.png"
                                            alt="comment-icon"
                                        />{" "}
                                    </button>
                                </div>
                            </li>
                        );
                    })
                    : null}
            </ul>
        </div>
    )
}