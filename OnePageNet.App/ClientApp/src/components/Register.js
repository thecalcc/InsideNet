import React, { useState, useEffect } from "react";


async function onSubmit(props){

  const registerDto = {
    email: props.email,
    password: props.password,
    confirmPassword: props.confirmPassword,
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

export const Register = (props) => {

  // this.onSubmit = this.onSubmit.bind(this);

  const [email, setEmail] = useState();
  const [password, setPassword] = useState();
  const [confirmPassword, setConfirmPassword] = useState();

  return (
    <form onSubmit={onSubmit({email, password, confirmPassword})}>
      <div className="container">
        <h1>Register</h1>
        <p>Please fill in this form to create an account.</p>

        <label>
          <b>Email</b>
        </label>
        <input
          name="email"
          type="email"
          value={this.email}
          onChange={(e) => setEmail(e.target.value)}
        />

        <label>
          <b>Password</b>
        </label>
        <input
          name="password"
          type="password"
          value={this.password}
          onChange={(e) => setPassword(e.target.value)}
        />

        <label>
          <b>Repeat Password</b>
        </label>
        <input
          name="confirmPassword"
          type="password"
          value={this.confirmPassword}
          onChange={(e) => setConfirmPassword(e.target.value)}
        />

        <button type="submit" className="registerbtn">
          Register
        </button>
      </div>
    </form>
  );
}
