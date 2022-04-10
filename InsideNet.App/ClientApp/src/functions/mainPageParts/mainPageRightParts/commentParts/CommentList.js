import React, { useState, useEffect } from "react";
import { CommentListItem } from "./CommentListItem";
import { CreateComment } from "./CreateComment";
import "../../../styles/CommentList.css"
export function CommentList({idOfPost}) {
  const [comments, setComments] = useState();
  const [users, setUsers] = useState();
  const [rerender,setRerender] = useState(false);
  const onRerender = () => {
    setRerender(!rerender);
  }
  useEffect(() => {
    const fetchComments = async () => {
      const urlPosts = `https://localhost:7231/api/Comments/get-by-id/${idOfPost}`;
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
  },[rerender, idOfPost]);

  const getPosterName = (posterId) => {
    return users?.find((user) => user.id === posterId).userName;
  };

  return (
    <>
    <CreateComment idOfPost={idOfPost} onRerender = {onRerender}/>
    <ul className="comment-list">
      {(comments !== null && comments !== undefined && comments !== "There are no such entities in the database.")? comments?.map((comment) => {
        return (
          <li className="comment">
            <CommentListItem
              comment={comment}
              poster={getPosterName(comment.applicationUserId)}
              isMyComment = "false"
              idOfPost = {idOfPost}
              onRerender = {onRerender}
            />
          </li> 
        );
      }) :
      <li>No comments yet</li>}
    </ul>
    </>
  );
}
