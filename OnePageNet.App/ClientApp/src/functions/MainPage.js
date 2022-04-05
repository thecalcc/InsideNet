import React, { useState } from 'react';
import { MainPageLeft } from './mainPageParts/MainPageLeft.js';
import { MainPageCenter } from './mainPageParts/MainPageCenter.js';
import { MainPageRight } from './mainPageParts/MainPageRight.js';
import "./styles/MainPage.css"

export function MainPage({ currentLayout, onLayoutChange }) {
    return (
        <>
            <div className='main-page'>
                <MainPageLeft/>
                <MainPageCenter className='main-page-center' onLayoutChange={onLayoutChange} currentLayout = {currentLayout.center}/>
                <MainPageRight layoutState={currentLayout.right}/>
            </div>
        </>
    );
};