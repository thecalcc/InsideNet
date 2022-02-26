export class RegisterDto {
  constructor(Email, Password, ConfirmPassword) {
      this.email = Email;
      this.password = Password;
      this.confirmPassword = ConfirmPassword;
    }
}
