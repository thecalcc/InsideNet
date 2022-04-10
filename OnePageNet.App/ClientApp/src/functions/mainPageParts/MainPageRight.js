import React, { useState } from "react";
import "../styles/MainPage.css";
import "../styles/PostList.css";
import { Users } from "./mainPageRightParts/userParts/Users";
import { PostListItem } from "./mainPageCenterParts/postCenterParts/PostListItem";
import { CommentList } from "./mainPageRightParts/commentParts/CommentList";
import { EditPost} from "./mainPageRightParts/postRightParts/EditPost"
import { useEffect } from "react";
import { UserProfile } from "./mainPageRightParts/userParts/UserProfile";

export function MainPageRight({ layoutState, post, users, onLayoutChange, user, choosePost }) {
  const [internalPost, setInternalPost] = useState(post);
  const isMyPost = (posterId) => {
    return posterId == sessionStorage.currentUserId;
  };
  const getPoster = (posterId) => {
    return users?.find((user) => user.id === posterId);
  };
  useEffect(()=>{
      setInternalPost(post);
  },[post])
  useEffect(() => {
    const fetchPost = async () => {
        const urlPosts = `https://localhost:7231/api/Posts/get/${post.id}`;
        await fetch(urlPosts, {
          method: "GET",
          mode: "cors",
          headers: {
            "Content-Type": "application/json",
            Accept: "application/json",
            "Access-Control-Allow-Origin": "*",
          },
        })
          .then((data) => data.json())
          .then((data) => setInternalPost(data));
      };
  
      if(post && (layoutState === "post" || layoutState === "edit"))fetchPost(); 
  },[onLayoutChange])
  return (
    <div className="main-page-right">
      {(() => {
        switch (layoutState) {
          case "users":
            return <Users />;
          case "post":
            return (
              <>
                {internalPost !== null ? (
                  <div className = "full-post-view">
                    <div className="post">
                      <PostListItem
                        post={internalPost}
                        poster={getPoster(internalPost.posterId)}
                        isMyPost={isMyPost(internalPost.posterId)}
                        onLayoutChange={onLayoutChange}
                      />
                    </div>
                    <div className="comments-view">
                      <CommentList idOfPost={internalPost.id} />
                    </div>
                  </div>
                ) : (
                  <>Ran into an error loading the post</>
                )}
              </>
            );
          case "edit":
            return (
              <>
                {post !== null ? (
                  <>
                    <EditPost post = {internalPost} onLayoutChange={onLayoutChange}/>
                    <button onClick={() => onLayoutChange("post", "right")}>
                      Back
                    </button>
                  </>
                ) : (
                  <>Ran into an error editing the post</>
                )}
                {/* <button onClick={() => setCurrentPost(null)}>Back</button> */}
              </>
            );
            case 'profile':
              return (
              <UserProfile 
                user = {user}
                post={internalPost}
                onLayoutChange={onLayoutChange}
                onSelect = {choosePost}
              />)
        }
      })()}
    </div>
  );
}
