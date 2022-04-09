import { useState } from "react";
import '../../../styles/UserSettings.css';

export function AccountSettingsEdit({ user, onLayoutChange }) {
  const stars = "********";
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
        email: user.email,
        id: user.id,
        oldPassword,
        newPassword,
      }),
    });
    onLayoutChange("settings-account", "center");
  };

  return (
    <>
      <form className="edit-form" onSubmit={(e) => handleSubmit(e)}>
        <fieldset>
          <label>
            <b>Username</b>
          </label>
          <input
            className='text-input'
            name="userName"
            type="text"
            value={userName}
            onChange={(e) => setUserName(e.target.value)}
          />
        </fieldset>

        <fieldset>
          <label>
            <b>Old Password</b>
          </label>
          <input
            className='text-input'
            name="oldPass"
            type="text"
            value={oldPassword}
            onChange={(e) => setOldPass(e.target.value)}
            required
          />
        </fieldset>

        <fieldset>

          <label>
            <b>New Password</b>
          </label>
          <input
            className='text-input'
            name="newPass"
            type="text"
            value={newPassword}
            onChange={(e) => setNewPass(e.target.value)}
            required
          />
        </fieldset>
        <button type="submit">Done</button>
      </form>
    </>
  );
}
