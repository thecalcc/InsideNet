import '../../../styles/UserSettings.css';

export function AccountSettings({ accountSettings, onLayoutChange }) {

  return (
    <div className='settings'>
      <ul className='settings-list'>
        <li>Username: {accountSettings.userName}</li>
        <li>Password: ********</li>
      </ul>
      <button className = 'custom-btn'onClick={() => onLayoutChange('settings-account-edit', 'center')}>
        <img className='btn-img' src='/resources/edit-icon.png' alt = 'edit-icon'/>
      </button>
    </div>
  );
}
