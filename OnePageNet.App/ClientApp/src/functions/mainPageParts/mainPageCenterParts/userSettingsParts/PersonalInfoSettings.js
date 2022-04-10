import dateFormat from 'dateformat';
import '../../../styles/UserSettings.css';
export function PersonalInfoSettings({ user, onLayoutChange }) {

  return (
    <div className='settings'>
      <ul className='settings-list'>
        <li>First name: {user.firstName}</li>
        <li>Last name: {user.lastName}</li>
        <li>Birth day: {dateFormat(user.doB, "dddd, mmmm dS, yyyy")}</li>
        <li>Gender: {user.gender ? user.gender : 'unspecified'}</li>
      </ul>
      {(() => {
        if (user.id === sessionStorage.getItem('currentUserId')) {
          return <button className='custom-btn' onClick={() => onLayoutChange('settings-personal-info-edit', 'center')}>
            <img className='btn-img' src='/resources/edit-icon.png' alt='edit-icon' />
          </button>
        }
      })()}
    </div>
  );
}
