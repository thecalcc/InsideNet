import React, { useState } from "react";
import { Link } from "react-router-dom";
import { useHistory } from "react-router-dom";

export function EditComment({idOfPost, comment, doneWithEdit}) {
const [content, setContent] = useState(comment.content);
const ApplicationUserId = sessionStorage.currentUserId;
const mediaUri = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";
const postId = idOfPost;
const history = useHistory();
  const handleSubmit = async (e) => {
    e.preventDefault();
    const url = `https://localhost:7231/api/comments/update/${comment.id}`;

    await fetch(url, {
      method: "PUT",
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
        ApplicationUserId,
        id:comment.id
      }),
    })
      .then(() => history.push("/"))

      doneWithEdit()
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
          <input type="submit" value="Done" />
      </form>
    </>
  );
}
