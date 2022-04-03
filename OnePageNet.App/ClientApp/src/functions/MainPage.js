import React, { useState } from 'react';
import { MainPageLeft } from './mainPageParts/MainPageLeft';
import { MainPageCenter } from './mainPageParts/MainPageCenter';
import { MainPageRight } from './mainPageParts/MainPageRight';
import "./styles/MainPage.css"

export function MainPage({temp, setTemp}) {
    return (
        <>
            <div className='main-page'>
                <MainPageLeft/>
                <MainPageCenter className = 'main-page-center' temp = {temp} setTemp = {setTemp}/>
                <MainPageRight temp = {temp}/>
            </div>
        </>
    );
};