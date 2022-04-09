import React from "react";
import dateFormat from "dateformat";
import "../../../../custom.css";
import { AdvancedImage } from "@cloudinary/react";
import { Cloudinary } from "@cloudinary/url-gen";
import { fill } from "@cloudinary/url-gen/actions/resize";

export function PostListItem({ post, poster, isMyPost, onLayoutChange }) {
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
    };
    deletePost(id);
  };
  const cld = new Cloudinary({
    cloud: {
      cloudName: "dsybrqu8w",
    },
  });

  const myImage = cld.image("image/upload/v1649369455/samples/imagecon-group");

  return (
    <>
      <div className="post-title">
        <div className="post-poster">
          <h6>@</h6>
          <h3>{poster}</h3>
          {isMyPost ? (
            <>
              <button
                className="custom-btn"
                onClick={() => onLayoutChange("edit", "right")}
              >
                <img
                  className="btn-img"
                  src="/resources/edit-icon.png"
                  alt="edit-icon"
                />
              </button>
              <button className="custom-btn" onClick={() => onDelete(post.id)}>
                <img
                  className="btn-img"
                  src="/resources/delete-icon.png"
                  alt="delete-icon"
                />
              </button>
            </>
          ) : null}
        </div>
        {dateFormat(post.createdAt, "dddd, mmmm dS, yyyy")}
      </div>
      <AdvancedImage cldImg={myImage} />
      <div className="post-content">{post.text}</div>
    </>
  );
}
