import React, {Component} from 'react';
import {Route} from 'react-router';
import {Layout} from './components/Layout';
import {Home} from './components/Home';
import {Register} from './components/Register'
import {Login} from './components/Login'

import './custom.css'

export default class App extends Component {
    static displayName = App.name;

    constructor(props){
      super(props);

      this.state = {
        isLoggedIn: this.props.isLoggedIn,
      };
    }

    render() {
        return (
          <Layout>
            <Route exact path="/" component={Home} />
            {this.state.isLoggedIn === true ? (
              <Route exact path="/" component={Home}></Route>
            ) : (
              <>
                <Route exact path="/register" component={Register} />
                <Route exact path="/login" component={Login} />
              </>
            )}
          </Layout>
        );
    }
}
