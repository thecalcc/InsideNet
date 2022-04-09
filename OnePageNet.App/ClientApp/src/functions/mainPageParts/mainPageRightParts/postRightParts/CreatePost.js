import React, { useState } from "react";

export function CreatePost({ onLayoutChange }) {
  const [text, setText] = useState();
  const posterId = sessionStorage.currentUserId;
  const mediaUri = null;
  const commentsIds = null;
  const reactionId = null;
  const [selectedFile, setSelectedFile] = useState(null);

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
        posterId,
      }),
    });
    const formData = new FormData();
    formData.append('file', selectedFile);
    formData.append('tags', `tutorial`);
    formData.append('upload_preset', 'tutorial'); 
    formData.append('api_key', '945449812673199'); 

    await fetch("https://api.cloudinary.com/v1_1/dsybrqu8w/image/upload", {
      method: "post",
      body: formData,
    })
      .then((x) => console.log(x))
      .then((x) => x.json())
      .then((x) => console.log(x));

    onLayoutChange("timeline", "center");
  };

  return (
    <>
      <form onSubmit={(e) => handleSubmit(e)}>
        <label htmlFor="image">
          Image
          <input
            type="file"
            id="file"
            name="file"
            placeholder="Upload an Image"
            required
            value={selectedFile}
            onChange={(e) => setSelectedFile(e.target.file[0])}
          />
        </label>

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
