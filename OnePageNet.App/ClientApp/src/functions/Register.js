import React, { useState } from "react";
import { Link } from "react-router-dom";
import { useHistory } from "react-router-dom";
import "./styles/Register.css";
import "./styles/basics.css";

export function Register({ setToken }) {
  const [email, setEmail] = useState();
  const [password, setPassword] = useState();
  const [confirmPassword, setConfirmPassword] = useState();
  const [userName, setUserName] = useState();
  const [firstName, setFirstName] = useState();
  const [lastName, setLastName] = useState();
  const [DoB, setDoB] = useState();
  const [gender, setGender] = useState();
  const [phoneNumber, setPhoneNumber] = useState();

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
      body: JSON.stringify({
        email,
        password,
        confirmPassword,
        DoB,
        firstName,
        lastName,
        userName,
        gender,
        phoneNumber,
      }),
    })
      .then((response) => response.json())
      .then((data) => {
        if (data.include("ey")) {
          setToken(data);
          sessionStorage.setItem("token", data);
        } 
      })
      .then((data) => {
        if (!data.ok) {
          throw new Error("Ooopsie...");
        }
      })
      .catch((e) => console.log(e));
  };

  return (
    <>
      <form className="form" onSubmit={(e) => handleSubmit(e)}>
        <div className="cont">
          <h1>Register</h1>
          <p>Please fill in this form to create an account.</p>

          <label>
            <b>Username</b>
          </label>
          <input
            name="userName"
            type="text"
            value={userName}
            onChange={(e) => setUserName(e.target.value)}
            required
          />

          <label>
            <b>Email</b>
          </label>
          <input
            name="email"
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />

          <label>
            <b>Password</b>
          </label>
          <input
            name="password"
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />

          <label>
            <b>Repeat Password</b>
          </label>
          <input
            name="confirmPassword"
            type="password"
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
            required
          />

          <label>
            <b>Date Of Birth</b>
          </label>
          <input
            name="DoB"
            type="date"
            value={DoB}
            onChange={(e) => setDoB(e.target.value)}
            required
          />

          <label>
            <b>First Name</b>
          </label>
          <input
            name="firstName"
            type="text"
            value={firstName}
            onChange={(e) => setFirstName(e.target.value)}
            required
          />

          <label>
            <b>Last Name</b>
          </label>
          <input
            name="lastName"
            type="text"
            value={lastName}
            onChange={(e) => setLastName(e.target.value)}
            required
          />

          <label>
            <b>Phone</b>
          </label>
          <input
            name="phone"
            type="tel"
            value={phoneNumber}
            onChange={(e) => setPhoneNumber(e.target.value)}
            required
          />
          <label>
            <b>Gender</b>
          </label>
          <div className="gender">
            <label>
              <input
                type="radio"
                value="Male"
                checked={gender === "Male"}
                onChange={(e) => setGender(e.target.value)}
                required
              />
              Male
            </label>
            <label>
              <input
                type="radio"
                value="Female"
                checked={gender === "Female"}
                onChange={(e) => setGender(e.target.value)}
                required
              />
              Female
            </label>
            <label>
              <input
                type="radio"
                value="Other"
                checked={gender === "Other"}
                onChange={(e) => setGender(e.target.value)}
                required
              />
              Other
            </label>
          </div>
          <input type="submit" value="Register" />
        </div>
      </form>
      <Link to="/">Back</Link>
    </>
  );
}
