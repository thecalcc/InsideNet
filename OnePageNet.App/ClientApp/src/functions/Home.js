import React from 'react';
import { Link } from 'react-router-dom';
export function Home() {
  return (
    <div>
      <h1>Home</h1>
      <p>
        <Link to="/register">Register</Link><br/>
        <Link to="/login">Login</Link> 
      </p>
    </div>
  );
};
