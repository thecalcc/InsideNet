import React, { useState } from "react";
import { Link } from "react-router-dom";
import { useHistory } from "react-router-dom";
import "../../../../custom.css"
import "../../../styles/CommentList.css"

export function CreateComment({ idOfPost, onRerender }) {
  const [content, setContent] = useState();
  const ApplicationUserId = sessionStorage.currentUserId;
  const mediaUri = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";
  const postId = idOfPost;
  const history = useHistory();
  const handleSubmit = async (e) => {
    e.preventDefault();
    const url = "https://localhost:7231/api/comments/create";

    await fetch(url, {
      method: "POST",
      mode: "cors",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        "Access-Control-Allow-Origin": "*",
      },
      body: JSON.stringify({
        content,
        mediaUri,
        postId,
        ApplicationUserId
      }),
    })
      .then(() => history.push("/"))
      onRerender();
  };

  return (
    <form className="comment-form" onSubmit={(e) => handleSubmit(e)}>
      <input
        className="text-input"
        name="content"
        type="text"
        value={content}
        placeholder="Write your comment here..."
        onChange={(e) => setContent(e.target.value)}
      />
      {/*  */}
      <button className="custom-btn" type="submit"><img className = 'btn-img' src = '/resources/post-icon.png' alt ='post-icon'/> </button>
    </form>
  );
}
