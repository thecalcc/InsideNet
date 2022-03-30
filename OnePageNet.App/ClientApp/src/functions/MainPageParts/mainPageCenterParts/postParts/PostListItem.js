import React from "react"
import dateFormat from 'dateformat';


export function PostListItem({ post, poster, selectPost, isMyPost, deletePost}) {
  return (
    <>
      <div className="post-title">
        <div className="post-poster">
          <h6>@</h6>
          <h3>{poster}</h3>
          {isMyPost ? (
            <>
              <button onClick={() => selectPost(post, "edit")}>Edit</button>
              <button onClick={() => deletePost(post.id)}>Delete</button>
            </>
          ) : null}
        </div>
        {dateFormat(post.createdAt, "dddd, mmmm dS, yyyy")}
      </div>
      <div className="post-content">{post.text}</div>     
    </>
  );
}
