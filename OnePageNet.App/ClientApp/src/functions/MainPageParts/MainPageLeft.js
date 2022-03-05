import React from "react"
import { Chats } from "./Chats"
import { Groups } from "./Groups"
import "../styles/MainPage.css"
export function MainPageLeft() {
    return (
        <div className="main-page-left">
            <Chats />
            <Groups />
        </div>
    )
}