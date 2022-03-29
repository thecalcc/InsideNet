import React, { useState } from "react";
import { Link } from "react-router-dom";
import { useHistory } from "react-router-dom";

export function CreatePost() {
const [text, setText] = useState();
const posterId = sessionStorage.currentUserId;
const mediaUri = null;
const commentsIds = null;
const reactionId = null;
const history = useHistory();
  const handleSubmit = async (e) => {
    e.preventDefault();
    const url = "https://localhost:7231/api/posts/create";

    await fetch(url, {
      method: "POST",
      mode: "cors",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        "Access-Control-Allow-Origin": "*",
      },
      body: JSON.stringify({
        text,
        mediaUri,
        reactionId,
        commentsIds,
        posterId
      }),
    })
      .then((data) => data.json()).then(() => history.push("/"))
  };

  return (
    <>
      <form onSubmit={(e) => handleSubmit(e)}>
          <label>
            <b>Text</b>
          </label>
          <input
            name="text"
            type="text"
            value={text}
            onChange={(e) => setText(e.target.value)}
          />
          <input type="submit" value="post" />
      </form>
    </>
  );
}
