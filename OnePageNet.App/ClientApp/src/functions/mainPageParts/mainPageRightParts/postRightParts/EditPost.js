import React, { useState, useEffect } from "react";
import { useHistory } from "react-router-dom";

export function EditPost({ post, onLayoutChange }) {
    const [text, setText] = useState(post.text);
    const posterId = sessionStorage.currentUserId;
    const mediaUri = null;
    const commentsIds = null;
    const reactionId = null;
    const history = useHistory();
      const handleSubmit = async (e) => {
        e.preventDefault();
        const url = `https://localhost:7231/api/posts/update/${post.id}`;
    
        const editPost = async () => { await fetch(url, {
          method: "PUT",
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
            posterId,
            id:post.id,
          }),
        }).then(() => onLayoutChange("post", "right"))}

        editPost();
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
