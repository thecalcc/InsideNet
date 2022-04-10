import React from "react"
import dateFormat from 'dateformat';
import '../../../styles/PostList.css'
import "../../../../custom.css";

export function PostListItem({ post, poster, isMyPost, onLayoutChange, selectUser}) {
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

const onCheckProfile = () => {
  console.log(poster)
  selectUser(poster);
  onLayoutChange('profile', 'right');
}
  return (
    <>
      <div className="post-header">
        <div className="post-poster">
          <button className = 'check-profile' onClick ={() => onCheckProfile()}><h5>@</h5><h2>{poster.userName}</h2></button>
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
