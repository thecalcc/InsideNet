import { useState } from "react";
import '../../../styles/UserSettings.css';

export function ContactSettingsEdit({ user }) {
  const [email, setEmail] = useState(user.email);
  const [phoneNumber, setPhoneNumber] = useState(user.phoneNumber);
  const userName = user.userName;

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
        email,
        phoneNumber,
        userName: user.userName,
        id: user.id,
      }),
    });
  };
  return (
    <form className='edit-form' onSubmit={(e) => handleSubmit(e)}>
      
      <fieldset>
          <label>
            <b>Email</b>
          </label>
          <input
            className='text-input'
            name="email"
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </fieldset>

        <fieldset>
          <label>
            <b>Phone Number</b>
          </label>
          <input
            className='text-input'
            name="phoneNumber"
            type="text"
            value={phoneNumber}
            onChange={(e) => setPhoneNumber(e.target.value)}
            required
          />
        </fieldset>
        <button className='custom-btn' type = 'submit'>Done</button>
    </form>
  );
}
