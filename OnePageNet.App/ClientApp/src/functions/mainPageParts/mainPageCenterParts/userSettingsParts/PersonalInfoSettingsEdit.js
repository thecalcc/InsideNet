import { useState } from "react";

export function PersonalInfoSettingsEdit({ user }) {
  const [email, setEmail] = useState(user.email);
  const [userName, setUserName] = useState(user.userName);
  const [firstName, setFirstName] = useState(user.firstName);
  const [lastName, setLastName] = useState(user.lastName);
  const [dob, setDob] = useState(user.dob);

  const handleSubmit = async () => {
    const url = `https://localhost:7231/api/users/update/${user.id}`;

    await fetch(url, {
      method: "PUT",
      mode: "cors",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json",
        "Access-Control-Allow-Origin": "*",
      },
      body: JSON.stringify({
        userName,
        email,
        firstName,
        lastName,
        dob,
        id: user.id,
      }),
    });
  };

  return (
    <form className="form" onSubmit={(e) => handleSubmit(e)}>
      <div className="cont">
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
          <b>Date Of Birth</b>
        </label>
        <input
          name="DoB"
          type="date"
          value={dob}
          onChange={(e) => setDob(e.target.value)}
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
      </div>
      <button type="submit">Done</button>
    </form>
  );
}
