import React, { useState, useEffect } from "react";
import { CommentListItem } from "./CommentListItem";
export function CommentList({idOfPost}) {
  const [comments, setComments] = useState();
  const [users, setUsers] = useState();
  useEffect(() => {
    const fetchComments = async () => {
      const urlPosts = "https://localhost:7231/api/Comments/get-all";
      await fetch(urlPosts, {
        method: "GET",
        mode: "cors",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
          "Access-Control-Allow-Origin": "*",
        },
      })
        .then((data) => data.json())
        .then((data) => setComments(data));
    };
    const fetchUsers = async () => {
      const urlUsers = "https://localhost:7231/api/Users/get-all";
      await fetch(urlUsers, {
        method: "GET",
        mode: "cors",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
          "Access-Control-Allow-Origin": "*",
        },
      })
        .then((data) => data.json())
        .then((data) => setUsers(data));
    };

    fetchUsers();
    fetchComments();
  }, []);

  const getPosterName = (posterId) => {
    return users?.find((user) => user.id === posterId).userName;
  };

  return (
    <ul>
      {(comments !== null && comments !== undefined && comments !== "There are no such entities in the database.")? comments?.map((comment) => {
        return (
          <li>
            <CommentListItem
              comment={comment}
              poster={getPosterName(comment.applicationUserId)}
              isMyComment = "false"
              idOfPost = {idOfPost}
            />
          </li> 
        );
      }) :
      <li>No comments yet</li>}
    </ul>
  );
}
