import React from 'react';
import { MainPageLeft } from './mainPageParts/MainPageLeft';
import { MainPageCenter } from './mainPageParts/MainPageCenter';
import { MainPageRight } from './mainPageParts/MainPageRight';
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