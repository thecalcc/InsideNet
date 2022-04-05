import React from "react"
import dateFormat from 'dateformat';
import { useState } from "react";
import { EditComment } from "./EditComment";
import { Link } from "react-router-dom";

export function CommentListItem({ comment, poster, idOfPost}) {
    const [isBeingEdited, setIsBeingEdited] = useState(false);
    const doneWithEdit = () => {
        setIsBeingEdited(false);
    }
    const deleteComment = async (id) => {
        const urlUsers = `https://localhost:7231/api/comments/delete/${id}`;
        await fetch(urlUsers, {
          method: "DELETE",
          mode: "cors",
          headers: {
            "Content-Type": "application/json",
            Accept: "application/json",
            "Access-Control-Allow-Origin": "*",
          },
        });
      };

  return (
    <>
      {isBeingEdited ? (
        <EditComment doneWithEdit = {doneWithEdit} comment = {comment} idOfPost = {idOfPost}/>
      ) : (
        <>
          <div className= "comment-title">
            <div className = "comment-commenter">
              <h6>@</h6>
              <h5>{poster}</h5>
            </div>
            <h6>
              {dateFormat(comment.createdAt, "dddd, mmmm dS, yyyy")}
            </h6>
            <ul className="comment-functions">
            {comment.applicationUserId == sessionStorage.currentUserId ? (
              <>
                <li><Link className="comment-link" onClick={() => setIsBeingEdited(true)}>Edit</Link></li>
                <li>
                  <Link className="comment-link" onClick={() => deleteComment(comment.id)}>Delete</Link>
                  </li>
              </>
            ) : null} 
            </ul>
            </div>
          <div className="comment-content">
            {comment.content}
          </div>
        </>
      )}
    </>
  );
}
