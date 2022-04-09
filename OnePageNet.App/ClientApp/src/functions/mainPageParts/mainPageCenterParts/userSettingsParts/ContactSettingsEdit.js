export function ContactEdit({user}) {

    return (
    <ul>
      <li><h2>E-mail: {user.email}</h2></li>
      <li><h2>phone number: {user.phoneNumber}</h2></li>
    </ul>
  );
}
