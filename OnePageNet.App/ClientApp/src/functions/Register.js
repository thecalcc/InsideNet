import { data } from "jquery";
import React, { useState } from "react";

async function registerUser(credentials) {
  const url = "https://localhost:7231/api/Account/register";

  return fetch(url, {
    method: "POST",
    mode: "cors",
    headers: {
      "Content-Type": "text/json",
      "Access-Control-Allow-Origin": "*",
    },
    body: JSON.stringify(credentials),
  }).then((data) => data.json());
}

export function Register({ setToken }) {
  const [email, setEmail] = useState();
  const [password, setPassword] = useState();
  const [confirmPassword, setConfirmPassword] = useState();
  // TODO const history = useHistory();

  const handleSubmit = async (e) => {
    const url = "https://localhost:7231/api/Account/register";

    fetch(url, {
      method: "POST",
      mode: "cors",
      headers: {
        "Content-Type": "text/json",
        "Access-Control-Allow-Origin": "*",
      },
      body: JSON.stringify({ email, password, confirmPassword }),
    }).then((data) => setToken(data.json()));

    try {
      // TODO then(history.push("/"));
    } catch (e) {
      console.log(e.error);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
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
