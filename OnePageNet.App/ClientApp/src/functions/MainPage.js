import React, { useState } from 'react';
import { MainPageLeft } from './mainPageParts/MainPageLeft';
import { MainPageCenter } from './mainPageParts/MainPageCenter';
import { MainPageRight } from './mainPageParts/MainPageRight';
import "./styles/MainPage.css"

export function MainPage({ currentLayout, onLayoutChange }) {
    return (
        <>
            <div className='main-page'>
                <MainPageLeft/>
                <MainPageCenter className='main-page-center' onLayoutChange={onLayoutChange}/>
                <MainPageRight layoutState={currentLayout}/>
            </div>
        </>
    );
};