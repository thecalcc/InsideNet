import React, { useState } from "react";
import { Link } from "react-router-dom";
import { useHistory } from "react-router-dom";

export function CreateComment({idOfPost}) {
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
  };

  return (
    <>
      <form onSubmit={(e) => handleSubmit(e)}>
          <label>
            <b>Text</b>
          </label>
          <input
            name="content"
            type="text"
            value={content}
            onChange={(e) => setContent(e.target.value)}
          />
          <input type="submit" value="post" />
      </form>
    </>
  );
}
