export class RegisterRequest {
  firstname!: string;
  lastname!: string;
  email!: string;
  phonenumber!: string;
  password!: string;
  confirmPassword!: string;
}

export class LoginRequest {
  email!: string;
  password!: string;
}

export class ResetPasswordRequest {
  email!: string;
  password!: string;
  confirmPassword!: string;
}

export class ChangePasswordRequest {
  oldPassword!: string;
  newPassword!: string;
  confirmPassword!: string;
}

export class UpdateUserRequest {
  firstname!: string;
  lastname!: string;
  phoneNumber!: string;
}
