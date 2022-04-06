import React, { useState, useEffect } from "react";
import { CommentListItem } from "./CommentListItem";
import "../../../styles/CommentList.css"
export function CommentList({idOfPost}) {
  const [comments, setComments] = useState();
  const [users, setUsers] = useState();
  const [rerender,setRerender] = useState(false);
  const onRerender = () => {
    console.log("boli me rumen radeviq")
  }
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
  },[]);

  const getPosterName = (posterId) => {
    return users?.find((user) => user.id === posterId).userName;
  };

  return (
    <ul className="comment-list">
      {(comments !== null && comments !== undefined && comments !== "There are no such entities in the database.")? comments?.map((comment) => {
        if(comment.postId == idOfPost)
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
  );
}
