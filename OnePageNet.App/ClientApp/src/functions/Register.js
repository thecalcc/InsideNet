import React, { useState } from "react";
import useToken from "./useToken";

export function Register({ setToken }) {
  const [email, setEmail] = useState();
  const [password, setPassword] = useState();
  const [confirmPassword, setConfirmPassword] = useState();
  // TODO const history = useHistory();

  const handleSubmit = async (e) => {
    e.preventDefault();
    const url = "https://localhost:7231/api/authentication/register";

    await fetch(url, {
      method: "POST",
      mode: "cors",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        "Access-Control-Allow-Origin": "*",
      },
      body: JSON.stringify({ email, password, confirmPassword }),
    })
      .then((data) => data.json())
      .then((data) => setToken(data));

    try {
      // TODO then(history.push("/"));
    } catch (e) {
      console.log(e.error);
    }
  };

  return (
    <form onSubmit={(e) => handleSubmit(e)}>
      <div className="container">
        <h1>Register</h1>
        <p>Please fill in this form to create an account.</p>

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

        <label>
          <b>Repeat Password</b>
        </label>
        <input
          name="confirmPassword"
          type="password"
          value={confirmPassword}
          onChange={(e) => setConfirmPassword(e.target.value)}
        />

        <input type="submit" value="Register" />
      </div>
    </form>
  );
}
