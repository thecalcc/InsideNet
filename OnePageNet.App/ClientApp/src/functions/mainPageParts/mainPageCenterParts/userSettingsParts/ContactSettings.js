export function ContactSettings({user}) {
  return (
    <ul>
      <li><h2>E-mail: {user.email}</h2></li>
      <li><h2>phone number: {(user.phoneNumber)?user.phoneNumber:"add phone number"}</h2></li>
    </ul>
  );
}
