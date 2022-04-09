

export function ContactSettings({ user, onLayoutChange }) {
  return (
    <div className='settings'>
      <ul className='settings-list'>
        <li>E-mail: {user.email}</li>
        <li>phone number: {(user.phoneNumber) ? user.phoneNumber : "No phone number"}</li>
      </ul>
      <button className = 'custom-btn'onClick={() => onLayoutChange('settings-about-edit', 'center')}>
        <img className='btn-img' src='/resources/edit-icon.png' alt = 'edit-icon'/>
      </button>
    </div>
  );
}
