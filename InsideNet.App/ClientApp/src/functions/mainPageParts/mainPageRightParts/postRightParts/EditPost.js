import React, { useState } from "react";

export function EditPost({ post, onLayoutChange }) {
    const [text, setText] = useState(post.text);
    const [title, setTitle] = useState(post.title);
    const posterId = sessionStorage.currentUserId;
    const mediaUri = null;
    const commentsIds = null;
    const reactionId = null;
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
            title
          }),
        }).then(() => onLayoutChange("post", "right"))}

        editPost();
      };
    
      return (
        <>
          <form onSubmit={(e) => handleSubmit(e)}>
              <label>
                <h2>Title</h2>
              </label>
              <input
                name="title"
                type="text"
                value={title}
                onChange={(e) => setTitle(e.target.value)}
              />
              <br/>
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
