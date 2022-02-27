import React from "react";
import { useHistory } from "react-router-dom";

export class Register extends React.Component {
  static displayName = Register.name;

  constructor(props) {
    super(props);

    this.state = {
      isLoggedIn: this.props.isLoggedIn,
    };

    history = useHistory();
    this.onSubmit = this.onSubmit.bind(this);
  }

  handleInputChange(event) {
    const target = event.target;
    const value = target.type === "checkbox" ? target.checked : target.value;
    const name = target.name;
    this.setState({
      [name]: value,
    });
  }

  async onSubmit(e) {

    const registerDto = {
      email: this.state.email,
      password: this.state.password,
      confirmPassword: this.state.confirmPassword,
    };

    const url = "https://localhost:7231/api/Account/register";

    await fetch(url, {
      method: "POST",
      mode: "cors",
      headers: {
        "Content-Type": "text/json",
        "Access-Control-Allow-Origin": "*",
      },
      body: JSON.stringify(registerDto),
    })
      .then((response) => response.json())
      .then((response) => {
        console.log(response);
        this.state.isLoggedIn = true;
      });
  }

  render() {
    return (
      <form onSubmit={this.onSubmit}>
        <div className="container">
          <h1>Register</h1>
          <p>Please fill in this form to create an account.</p>

          <label>
            <b>Email</b>
          </label>
          <input
            name="email"
            type="email"
            value={this.state.email}
            onChange={(e) => this.handleInputChange(e)}
          />

          <label>
            <b>Password</b>
          </label>
          <input
            name="password"
            type="password"
            value={this.state.pass}
            onChange={(e) => this.handleInputChange(e)}
          />

          <label>
            <b>Repeat Password</b>
          </label>
          <input
            name="confirmPassword"
            type="password"
            value={this.state.passrepeat}
            onChange={(e) => this.handleInputChange(e)}
          />

          <button type="submit" className="registerbtn">
            Register
          </button>
        </div>
      </form>
    );
  }
}
