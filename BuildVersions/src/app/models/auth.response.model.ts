export class RegisterResponse {
  url!: string;
}

export class AcknowledgeUserResponse {
  token!: string;
  constructor(token: string) {
    this.token = token;
  }
}

export class LoginResponse {
  guid!: string;
  fullname!: string;
  roles!: string[];
  jwtToken!: string;
  refreshToken!: string;
  token!: string;
  email: string | undefined;
}

export class ResetPasswordResponse {
  url!: string;
}

export class AcknowledgePasswordResponse {
  token!: string;
  constructor(token: string) {
    this.token = token;
  }
}

export class ChangePasswordResponse {
  email!: string;
}

export class UpdateUserResponse {
  email!: string;
  firstname!: string;
  lastname!: string;
  phoneNumber!: string;
}
// export class LogoutResponse {
//   email!: string;
// }
