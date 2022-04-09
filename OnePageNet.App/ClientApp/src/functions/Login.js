import React, { useState } from "react";
import { useHistory } from "react-router-dom";
import '../custom.css'
import './styles/Login.css'
export function Login({ setToken, onLayoutChange }) {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const history = useHistory();

  const handleSubmit = async (e) => {
    e.preventDefault();
    const postUrl = "https://localhost:7231/api/authentication/login";

    await fetch(postUrl, {
      method: "POST",
      mode: "cors",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        "Access-Control-Allow-Origin": "*",
      },
      body: JSON.stringify({ email, password }),
    })
      .then((result) => result.json())
      .then((result) => {
        setToken(result.generatedToken);
        sessionStorage.setItem("token", result.generatedToken);
        sessionStorage.setItem("currentUserId", result.id);
      })
      .then(() => onLayoutChange("timeline", "center"))
      .then(history.push("/"));
  };

  return (
    <form onSubmit={(e) => handleSubmit(e)}>
      <div className='login-form'>
        <h1>Login</h1>
        <p>Please fill in this form to login to an account.</p>

        <fieldset className='login-fieldset'>
          <label>
            <b>Email</b>
          </label>
          <input
            className='text-input'
            name="email"
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </fieldset>

        <fieldset className = 'login-fieldset'>
          <label>
            <b>Password</b>
          </label>
          <input
            className='text-input'
            name="password"
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </fieldset>

        <button className='custom-btn' type="submit" value="Login" onClick={(e) => handleSubmit(e)}> Login </button>
      </div>
    </form>
  );
}
