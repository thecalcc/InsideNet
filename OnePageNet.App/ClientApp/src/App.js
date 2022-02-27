import React, {useState} from 'react';
import {Route} from 'react-router';
import {Layout} from './functions/Layout';
import {Home} from './functions/Home';
import {Register} from './functions/Register'
import {Login} from './functions/Login'

import './custom.css'


export const App = (props) => {
    const [isLoggedIn, setIsLoggedIn] = useState();

    return (
      <Layout>
        <Route exact path="/">
          <Home name={props.name} />
        </Route>
        {isLoggedIn === true ? (
          <Route exact path="/">
            <Home name={props.name} />
          </Route>
        ) : (
          <>
            <Route exact path="/register">
              <Register isLoggedIn={isLoggedIn} />
            </Route>
            <Route exact path="/login">
              <Login isLoggedIn={isLoggedIn} />
            </Route>
          </>
        )}
      </Layout>
    );
}
