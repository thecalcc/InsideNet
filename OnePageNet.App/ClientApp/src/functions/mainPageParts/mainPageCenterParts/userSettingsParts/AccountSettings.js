export function AccountSettings({accountSettings, onLayoutChange}) {

    return (
      <ul>
      <li><h2>Username: {accountSettings.userName}</h2></li>
      <li><h2>E-mail: {accountSettings.email}</h2></li>
      <li><h2>Password: ********</h2></li>
      <button onClick={()=>onLayoutChange("settings-account-edit","center")}>Edit</button>
    </ul>
    );
}
