import React from "react"
import dateFormat from 'dateformat';


export function PostListItem({ post, poster, isMyPost, onLayoutChange}) {
  const onDelete = (id) => {
  const deletePost = async (id) => {
    const urlUsers = `https://localhost:7231/api/posts/delete/${id}`;
    await fetch(urlUsers, {
      method: "DELETE",
      mode: "cors",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        "Access-Control-Allow-Origin": "*",
      },
    });
  }

  deletePost(id);
  onLayoutChange("", "right");
}
  return (
    <>
      <div className="post-title">
        <div className="post-poster">
          <h6>@</h6>
          <h3>{poster}</h3>
          {isMyPost ? (
            <>
              <button onClick={() => onLayoutChange("edit", "right")}>Edit</button>
              <button onClick={() => onDelete(post.id)}>Delete</button> 
            </>
          ) : null}
        </div>
        {dateFormat(post.createdAt, "dddd, mmmm dS, yyyy")}
      </div>
      <div className="post-content">{post.text}</div>
    </>
  );
}
