import React from "react";
import { useState } from "react";

export function EditGroup({ group, onChangeGroup }) {
  const [name, setName] = useState(group.name);
  const handleSubmit = async (e) => {
    e.preventDefault();
    const url = `https://localhost:7231/api/groups/update/${group.id}`;
    const Name = e.target.name.value;
    const MediaUri = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";
    await fetch(url, {
      method: "PUT",
      mode: "cors",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        "Access-Control-Allow-Origin": "*",
      },
      body: JSON.stringify({
        Name,
        MediaUri,
        id: group.id,
      }),
    })
    .then((data) => data.json())
    .then((data) => onChangeGroup(data));
  };

  return (
    <>
      <form className="edit-form" onSubmit={(e) => handleSubmit(e)}>
        <input
          className="text-input"
          name="name"
          type="text"
          value={name}
          onChange={(e) => setName(e.target.value)}
        />
        <button className="custom-btn" type="submit">
          <img
            className="btn-img"
            src="/resources/edit-icon.png"
            alt="edit-icon"
          />
        </button>
      </form>
    </>
  );
}
