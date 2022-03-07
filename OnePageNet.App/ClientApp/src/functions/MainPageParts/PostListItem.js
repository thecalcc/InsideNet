import React from "react"

export function PostListItem({ createdAt, poster, text}) {
    return (
        <div>
          <div>
            <h3>{poster}</h3>
            <h5>{createdAt}</h5>
          </div>
          <div>
            <p>
              <b>{text}</b>
            </p>
          </div>
        </div>
    );
}
