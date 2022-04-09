import { useState } from "react";
import '../../../styles/UserSettings.css';

export function PersonalInfoSettingsEdit({ user }) {
  const [firstName, setFirstName] = useState(user.firstName);
  const [lastName, setLastName] = useState(user.lastName);
  const [gender, setGender] = useState(user.gender);
  const [dob, setDob] = useState(user.dob);

  const userName = user.userName;
  const email = user.email;
  const phoneNumber = user.phoneNumber;
  const mediaUri = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";

  const handleSubmit = async (e) => {
    e.preventDefault();
    
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
        phoneNumber,
        gender,
        mediaUri
      }),
    });
  };

  return (
    <form className='edit-form' onSubmit={(e) => handleSubmit(e)}>
      <fieldset>
        <h1>Edit</h1>
        <p>Please fill in this form</p>
      </fieldset>

      <fieldset>
        <label>
          <b>Date Of Birth</b>
        </label>
        <input
          className='text-input'
          name="DoB"
          type="date"
          value={dob}
          onChange={(e) => setDob(e.target.value)}
          required
        />
      </fieldset>

      <fieldset>
        <label>
          <b>First Name</b>
        </label>
        <input
          className='text-input'
          name="firstName"
          type="text"
          value={firstName}
          onChange={(e) => setFirstName(e.target.value)}
          required
        />
      </fieldset>


      <fieldset>
        <label>
          <b>Last Name</b>
        </label>
        <input
          className='text-input'
          name="lastName"
          type="text"
          value={lastName}
          onChange={(e) => setLastName(e.target.value)}
          required
        />
      </fieldset>

      <fieldset>
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
        </fieldset>
      <button className='custom-btn' type="submit">Done</button>
    </form>
  );
}
