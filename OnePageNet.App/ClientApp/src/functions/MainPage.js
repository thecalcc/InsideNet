import React, { useState, useEffect } from 'react';
import { MainPageLeft } from './mainPageParts/MainPageLeft.js';
import { MainPageCenter } from './mainPageParts/MainPageCenter.js';
import { MainPageRight } from './mainPageParts/MainPageRight.js';
import "./styles/MainPage.css"

export function MainPage({ currentLayout, onLayoutChange }) {
    const [post, setPost] = useState(null)
    const [users, setUsers] = useState();
    const selectPost = (post) =>{
        setPost(post);
    }
    useEffect(() => {
        const fetchUsers = async () => {
          const urlUsers = "https://localhost:7231/api/Users/get-all";
          await fetch(urlUsers, {
            method: "GET",
            mode: "cors",
            headers: {
              "Content-Type": "application/json",
              Accept: "application/json",
              "Access-Control-Allow-Origin": "*",
            },
          })
            .then((data) => data.json())
            .then((data) => setUsers(data));
        };
        fetchUsers();
      }, []);
    return (
        <>
            <div className='main-page'>
                <MainPageLeft/>
                <MainPageCenter className='main-page-center' onLayoutChange={onLayoutChange} currentLayout = {currentLayout} selectPost = {selectPost} users={users}/>
                <MainPageRight layoutState={currentLayout.right} post={post} users = {users} onLayoutChange={onLayoutChange} changePost={selectPost} />
            </div>
        </>
    );
};