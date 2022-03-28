import React, { useState } from "react";
import { Link } from "react-router-dom";
import { useHistory } from "react-router-dom";

export function CreatePost() {
const [text, setText] = useState();
const PosterId = sessionStorage.currentUserId;
const MediaUri = null;
const CommentsIds = null;
const ReactionId = null;
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
        MediaUri,
        ReactionId,
        CommentsIds,
        PosterId
      }),
    })
      .then((data) => data.json());

    try {
      history.push("/");
    } catch (e) {
      console.log(e.error);
    }
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
