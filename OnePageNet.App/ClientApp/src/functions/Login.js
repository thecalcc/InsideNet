import React, { useState} from "react";
import { useHistory } from "react-router-dom";

async function loginUser(credentials) {
  const url = "https://localhost:7231/api/Account/login";
  return fetch(url, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(credentials)
  })
    .then(data => data.json())
 }

export function Login({setIsLoggedIn}) {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  // const history = useHistory();
  // const [isLoggedIn, setIsLoggedIn] = useState();


  const handleSubmit = async e => {
    e.preventDefault();
    const token = await loginUser({
      email,
      password
    });
    setIsLoggedIn(true);
  }

  return (
    <form onSubmit={handleSubmit}>
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

        <input type="submit" value="Login" />
      </div>
    </form>
  );
}