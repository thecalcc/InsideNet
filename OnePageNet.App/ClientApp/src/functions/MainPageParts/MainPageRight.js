import React, { useState } from "react"
import "../styles/MainPage.css"
import "../styles/PostList.css"
import { Users } from "./Users"

export function MainPageRight({ layoutState }) {
    console.log(layoutState);
    return(
        <div className="main-page-right">
            OOF
            {
                (() => {
                    switch(layoutState){
                        case "users":
                            return(
                                <Users/>
                            )
                        case "post":
                            return(
                                <div>
                                    <h1>KMS</h1>
                                    {/* <PostListItem
                                        selectPost={selectPost}
                                        post={currentPost}
                                        poster={getPosterName(currentPost.posterId)}
                                        isMyPost={isMyPost(currentPost.posterId)}
                                        deletePost={deletePost}
                                    />
                                    <CreateComment idOfPost = {currentPost.id}/>
                                    <CommentList idOfPost = {currentPost.id}/> */}
                                    {/* <button onClick={() => setCurrentPost(null)}>Back</button> */}
                                </div>
                            )
                    }
                    
                })()
            }
        </div>
    )
}