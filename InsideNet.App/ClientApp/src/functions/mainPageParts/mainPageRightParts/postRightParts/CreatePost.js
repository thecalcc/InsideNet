import React, { useState } from "react";
import '../../../styles/PostList.css'
export function CreatePost({ onLayoutChange }) {
  const [text, setText] = useState();
  const [title, setTitle] = useState();
  const posterId = sessionStorage.currentUserId;
  const mediaUri = null;
  const commentsIds = null;
  const reactionId = null;
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
        title,
        mediaUri,
        reactionId,
        commentsIds,
        posterId,
      }),
    });
    onLayoutChange("timeline", "center");
    setText('');
    setTitle('');
  };

  return (
    <form className='create-post-form' onSubmit={(e) => handleSubmit(e)}>
      <fieldset>
        <input
          className='text-input'
          name="text"
          type="text"
          placeholder='Give it a cool title...'
          value={title}
          onChange={(e) => setTitle(e.target.value)}
        />
        <input
          className='text-input'
          name="text"
          type="text"
          placeholder='Write your own post...'
          value={text}
          onChange={(e) => setText(e.target.value)}
        />
      </fieldset>
      <button className='custom-btn' type="submit" value="post"><img className='btn-img' src='/resources/post-icon.png' alt='post-icon' /></button>
    </form>
  );
}
