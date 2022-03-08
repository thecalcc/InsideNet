import React from "react"
import "../styles/MainPage.css"
import { PostList } from "./PostList"

export function MainPageCenter(){
    return(
        <div className = 'main-page-center'>
            <PostList/>
        </div>

    )
}