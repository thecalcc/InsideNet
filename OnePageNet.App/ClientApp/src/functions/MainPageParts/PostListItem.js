import React from "react"
import dateFormat from 'dateformat';

export function PostListItem({ createdAt, poster, text }) {
  return (
    <li className="post">
      <div className="post-title">
        <div className="post-poster">
          <h6>@</h6>
          <h3>{poster}</h3>
        </div>
        {dateFormat(createdAt, "dddd, mmmm dS, yyyy")}
      </div>
      <div className="post-content">
        {text}
      </div>
    </li>
  );
}
