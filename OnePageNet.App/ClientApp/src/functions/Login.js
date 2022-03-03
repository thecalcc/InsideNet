import axios from "axios";
import React, { useState } from "react";
import { useHistory } from "react-router-dom";

export function Login({ setToken }) {
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
      .then((result) => setToken(result));
  };

  return (
    <form onSubmit={(e) => handleSubmit(e)}>
      <div className="container">
        <h1>Login</h1>
        <p>Please fill in this form to login to an account.</p>

        <label>
          <b>Email</b>
        </label>
        <input
          name="email"
          type="email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />

        <label>
          <b>Password</b>
        </label>
        <input
          name="password"
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />

        <input type="submit" value="Login" onClick={(e) => handleSubmit(e)} />
      </div>
    </form>
  );
}
