import React from 'react';
import { MainPageLeft } from './MainPageParts/MainPageLeft';
import { MainPageCenter } from './MainPageParts/MainPageCenter';
import { MainPageRight } from './MainPageParts/MainPageRight';


export function MainPage(props) {
  return (
    <div>
        <h1>Main</h1>
      <MainPageLeft/>
      <MainPageCenter/>
      <MainPageRight/>
    </div>
  );
};