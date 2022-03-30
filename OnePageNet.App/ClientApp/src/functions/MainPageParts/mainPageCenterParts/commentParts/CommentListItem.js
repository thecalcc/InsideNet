import React from "react"
import dateFormat from 'dateformat';
import { useState } from "react";
import { EditComment } from "./EditComment";

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
          <div>
            <div>
              <h3>{poster}</h3>
              {comment.applicationUserId == sessionStorage.currentUserId ? (
                <>
                  {<button onClick={() => setIsBeingEdited(true)}>Edit</button>}
                  <button onClick={() => deleteComment(comment.id)}>
                    Delete
                  </button>
                </>
              ) : null}
            </div>
            {dateFormat(comment.createdAt, "dddd, mmmm dS, yyyy")}
          </div>
          <div>{comment.content}</div>
        </>
      )}
    </>
  );
}
