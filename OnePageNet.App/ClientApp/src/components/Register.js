import axios from "axios";
import React, { Component } from "react";
import { useState } from "react";

export class Register extends Component {
  static displayName = Register.name;
  constructor(props) {
    super(props);
    this.state = {
      error: null,
      isLoaded: false,
      items: [],
    };

    this.onSubmit = this.onSubmit.bind(this);
  }

  delta = () => {
    this.setState({
      count: this.state.count + 1,
    });
  };

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
      Email: this.state.email,
      Password: this.state.pass,
      ConfirmPassword: this.state.passrepeat,
    };

      axios.post("https://localhost:7231/api/Account/register/", {
         ...registerDto
      });


      axios.post("https://localhost:7231/api/Account/register/detroit");

    /*axios({
      method: "post",
      url: "https://localhost:7231/api/Account/register",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json;charset=UTF-8",
      },
      data: {
        ...registerDto
      },
    });*/
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
            name="pass"
            type="password"
            value={this.state.pass}
            onChange={(e) => this.handleInputChange(e)}
          />

          <label>
            <b>Repeat Password</b>
          </label>
          <input
            name="passrepeat"
            type="password"
            value={this.state.passrepeat}
            onChange={(e) => this.handleInputChange(e)}
          />

          <button type="submit" className="registerbtn">
            Register
          </button>
        </div>

        <div className="container signin">
          <p>
            Already have an account? <a href="#">Sign in</a>.
          </p>
        </div>
      </form>
    );
  }
}
