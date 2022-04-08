import React, { useState } from 'react';
import { Login } from './Login.js';
import { Register } from './Register.js';
import './styles/HomePage.css';
export function Home({setToken, onLayoutChange}) {
  const [homeState, setHomeState] = useState('login');

  return (
    <div className='home'>
      <div className='login-register-container'>
        {
          (() => {
            if (homeState === 'login') {
              return <Login setToken={setToken} onLayoutChange={onLayoutChange}/>
            } else {
              return <Register setToken={setToken}/>
            }
          })()
        }
        <div className='buttons'>
          <button className='button' onClick={() => setHomeState('register')}>Register</button>
          <button className='button' onClick={() => setHomeState('login')}>Login</button>
        </div>
      </div>
    </div>
  );
};
