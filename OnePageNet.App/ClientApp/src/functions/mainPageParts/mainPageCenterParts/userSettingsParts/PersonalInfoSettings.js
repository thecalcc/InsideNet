export function PersonalInfoSettings({ user,  onLayoutChange}) {

  return (
    <ul>
      <li><h2>First name: {user.firstName}</h2></li>
      <li><h2>Last name: {user.lastName}</h2></li>
      <li><h2>Birth day: {user.doB}</h2></li>
      <li><h2>Gender: {(user.gender)?user.gender:"specify gender"}</h2></li>
      <button onClick={()=>onLayoutChange("settings-personal-info-edit","center")}>Edit</button>
    </ul>
  );
}
