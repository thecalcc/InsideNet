import React from "react"
import dateFormat from 'dateformat';
import { useState } from "react";
import { EditComment } from "./EditComment";
import { Link } from "react-router-dom";
import '../../../../custom.css'

export function CommentListItem({ comment, poster, idOfPost, onRerender }) {
  const [isBeingEdited, setIsBeingEdited] = useState(false);
  const doneWithEdit = () => {
    setIsBeingEdited(false);
    onRerender();
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
    onRerender();
  };

  return (
    <>
      {isBeingEdited ? (
        <EditComment doneWithEdit={doneWithEdit} comment={comment} idOfPost={idOfPost} />
      ) : (
        <>
          <div className="comment-title">
            <div className="comment-commenter">
              <h6>@</h6>
              <h5>{poster}</h5>
            </div>
            <h6>
              {dateFormat(comment.createdAt, "dddd, mmmm dS, yyyy")}
            </h6>
            {comment.applicationUserId == sessionStorage.currentUserId ? (
              <div className='comment-functions'>
                <button className="custom-btn" onClick={() => setIsBeingEdited(true)}><img className='btn-img' src='/resources/edit-icon.png' alt='edit-icon' /></button>
                <button className="custom-btn" onClick={() => deleteComment(comment.id)}><img className='btn-img' src='/resources/delete-icon.png' alt='delete-icon' /></button>
              </div>
            ) : null}
          </div>
          <div className="comment-content">
            {comment.content}
          </div>
        </>
      )}
    </>
  );
}
