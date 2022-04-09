import React from "react"
import dateFormat from 'dateformat';
import "../../../../custom.css";

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
    }).then(() => onLayoutChange("", "right"));
  }
  deletePost(id);
}
  return (
    <>
      <div className="post-header">
        <div className="post-poster">
          <h6>@</h6>
          <h2>{poster}</h2>
          {isMyPost ? (
            <>
              <button className = 'custom-btn' onClick={() => onLayoutChange("edit", "right")}><img className = 'btn-img' src ='/resources/edit-icon.png' alt = 'edit-icon'/></button>
              <button className = 'custom-btn' onClick={() => onDelete(post.id)}><img className = 'btn-img' src ='/resources/delete-icon.png' alt = 'delete-icon'/></button> 
            </>
          ) : null}
        </div>
        {dateFormat(post.createdAt, "dddd, mmmm dS, yyyy")}
      </div>
      <div className = 'post-title'>{post.title}</div>
      <div className="post-content">{post.text}</div>
    </>
  );
}
