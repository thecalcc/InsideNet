import { useState } from "react";
export function AccountSettingsEdit({ user }) {
  const stars = "********";
  const [email, setEmail] = useState(user.email);
  const [userName, setUserName] = useState(user.userName);
  const [newPassword, setNewPass] = useState(stars);
  const [oldPassword, setOldPass] = useState(stars);

  const handleSubmit = async (e) => {
    e.preventDefault();
    let url = `https://localhost:7231/api/accountsettings/update/${user.id}`;
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
        id: user.id,
        oldPassword,
        newPassword,
      }),
    });
  };

  return (
    <>
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
          />

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
            <b>Old Password</b>
          </label>
          <input
            name="oldPass"
            type="text"
            value={oldPassword}
            onChange={(e) => setOldPass(e.target.value)}
            required
          />

          <label>
            <b>New Password</b>
          </label>
          <input
            name="newPass"
            type="text"
            value={newPassword}
            onChange={(e) => setNewPass(e.target.value)}
            required
          />
        </div>
        <button type="submit">Done</button>
      </form>
    </>
  );
}
