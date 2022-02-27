import React, {Component, useState} from 'react';
import {Route} from 'react-router';
import {Layout} from './components/Layout';
import {Home} from './components/Home';
import {Register} from './components/Register'
import {Login} from './components/Login'

import './custom.css'


export const App = (props) => {
    const [isLoggedIn, setIsLoggedIn] = useState();

    return (
      <Layout>
        <Route exact path="/" component={Home} />
        {isLoggedIn === true ? (
          <Route exact path="/" component={Home}></Route>
        ) : (
          <>
            <Route exact path="/register"><Register isLoggedIn/></Route>
            <Route exact path="/login" component={Login} />
          </>
        )}
      </Layout>
    );
}
