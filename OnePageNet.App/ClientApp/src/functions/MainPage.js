import React from 'react';
import { MainPageLeft } from './MainPageParts/MainPageLeft';
import { MainPageCenter } from './MainPageParts/MainPageCenter';
import { MainPageRight } from './MainPageParts/MainPageRight';
import { Chat } from "./../chat/Chat";
import "./styles/MainPage.css"

export function MainPage(props) {
    return (
        <>
            <div className='main-page'>
                <Chat/>
                <MainPageLeft/>
                <MainPageCenter className = 'main-page-center'/>
                <MainPageRight/>
            </div>
        </>
    );
};