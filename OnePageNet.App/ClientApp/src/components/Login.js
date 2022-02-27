import React from 'react';

export class Login extends React.Component {

    constructor(props){
      super(props);
      this.state = {
        isLoggedIn: this.props.isLoggedIn,
      };
    }

    // render(){
    //     return (
    //       <>
    //         <button
    //           type="submit"
    //           value={this.state.isLoggedIn}
    //           onClick={this.state.isLoggedIn = false}
    //         ></button>
    //       </>
    //     );
    // }

}