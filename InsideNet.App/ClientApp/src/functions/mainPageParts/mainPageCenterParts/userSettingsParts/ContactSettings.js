import React from "react"
import '../../../styles/UserSettings.css';

export function ContactSettings({ user, onLayoutChange }) {
  return (
    <div className='settings'>
      <ul className='settings-list'>
        <li>E-mail: {user.email}</li>
        <li>Phone number: {(user.phoneNumber) ? user.phoneNumber : "No phone number"}</li>
      </ul>
      <button className = 'custom-btn'onClick={() => onLayoutChange('settings-contacts-edit', 'center')}>
        <img className='btn-img' src='/resources/edit-icon.png' alt = 'edit-icon'/>
      </button>
    </div>
  );
}
