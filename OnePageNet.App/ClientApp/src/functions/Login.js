import React, { useState} from "react";
import { useHistory } from "react-router-dom";

export function Login({setToken}) {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const history = useHistory();
  const [isLoggedIn, setIsLoggedIn] = useState();


  const handleSubmit = async () => {
    const postUrl = "https://localhost:7231/api/Account/login";

    const login = async () => {
      const res = fetch(postUrl, {
        method: "POST",
        mode: "cors",
        headers: {
          "Content-Type": "text/json",
          "Access-Control-Allow-Origin": "*",
        },
        body: JSON.stringify({ email, password }),
      });

      await res.then((response) => {        
        if (response.ok) {
          setIsLoggedIn(x => x = true);
        } else {
          console.log("Login failed");
          setIsLoggedIn(false);
        }
      });
    };

    try{
     await login().then(history.push("/"));
    } catch(e){
      console.log(e.error);
    }
  };

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