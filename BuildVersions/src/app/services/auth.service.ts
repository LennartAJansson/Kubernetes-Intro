import { Injectable, inject } from '@angular/core';
import { AuthApiService } from './auth.api.service';
import {
  LoginRequest,
  RegisterRequest,
  ResetPasswordRequest,
} from '../models/auth.request.model';
import {
  AcknowledgePasswordResponse,
  AcknowledgeUserResponse,
  LoginResponse,
  RegisterResponse,
  ResetPasswordResponse,
} from '../models/auth.response.model';
import { firstValueFrom, BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  //#region properties
  public currentUserSubject = new BehaviorSubject<LoginResponse | null>(null);
  public currentUser = this.currentUserSubject.asObservable();
  //#endregion

  //#region constructor
  private apiSvc = inject(AuthApiService);
  constructor() {
    this.loadInitailUser();
  }
  //#endregion

  loadInitailUser() {
    const userData = localStorage.getItem('currentUser');
    if (userData) {
      this.currentUserSubject.next(JSON.parse(userData));
    } else {
      this.currentUserSubject.next(null);
    }
  }

  getAuthToken(): string | null {
    const token = localStorage.getItem('access_token');
    console.log('Retrieved token');
    return token;
  }

  isAuthenticated(): boolean {
    const token = this.getAuthToken();
    console.log('Checking authentication status with token:');
    return !!token;
  }

  isAdmin(): boolean {
    const user = this.currentUserSubject.value;
    console.log('Checking if user is admin:', user);
    return user != null && user!.roles.includes('Admin');
  }

  async login(loginRequest: LoginRequest): Promise<LoginResponse> {
    const response = await firstValueFrom(this.apiSvc.login(loginRequest));
    console.log('Login Response:', response);
    if (response && response.jwtToken) {
      localStorage.setItem('access_token', response.jwtToken);
      localStorage.setItem('currentUser', JSON.stringify(response));
      // console.log('Token saved:', response.jwtToken);
      // console.log('logged in as:', response.username);
      this.currentUserSubject.next(response);
    } else {
      console.log('No token received:', response);
    }
    return response;
  }

  public async register(
    registerData: RegisterRequest
  ): Promise<RegisterResponse> {
    const response = await firstValueFrom(this.apiSvc.register(registerData));
    console.log('Sending registerresponse:', response);
    return response;
  }

  async acknowledgeNewUser(tokenUrl: string): Promise<AcknowledgeUserResponse> {
    return await firstValueFrom(this.apiSvc.acknowledgeUser(tokenUrl));
  }

  async acknowledgeNewPassword(
    tokenUrl: string
  ): Promise<AcknowledgePasswordResponse> {
    return await firstValueFrom(this.apiSvc.acknowledgePassword(tokenUrl));
  }

  async resetPassword(
    passwordRequest: ResetPasswordRequest
  ): Promise<ResetPasswordResponse> {
    const response = await firstValueFrom(
      this.apiSvc.resetPassword(passwordRequest)
    );
    console.log('Reset password token:', response);
    return response;
  }

  async logout(): Promise<void> {
    localStorage.removeItem('access_token');
    this.currentUserSubject.next(null);
  }

  // async logout(): Promise<void> {
  //   try {
  //     await firstValueFrom(this.apiSvc.logout());
  //     localStorage.removeItem('access_token');
  //     this.currentUserSubject.next(null);
  //   }
  //   catch (error) {
  //     console.error('Error logging out:', error);
  //     throw error;
  //   }
  // }
}
