import React, { useState} from "react";
import { useHistory } from "react-router-dom";

export function Register(props) {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [isLoggedIn, setIsLoggedIn] = useState();
  const history = useHistory();

  const handleSubmit = async () => {
    const url = "https://localhost:7231/api/Account/register";

    const register = async () => {
      const res = fetch(url, {
        method: "POST",
        mode: "cors",
        headers: {
          "Content-Type": "text/json",
          "Access-Control-Allow-Origin": "*",
        },
        body: JSON.stringify({ email, password, confirmPassword }),
      });

      await res.then((response) => {
        if (response.ok) {
          console.log(response.data);
          setIsLoggedIn(true);
        } else {
          console.log("Login failed");
          setIsLoggedIn(false);
        }
      });
    };

    try {
      await register().then(history.push("/"));
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