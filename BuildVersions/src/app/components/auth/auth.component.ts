import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { AuthService } from '../../services/auth.service';
import { LoginRequest, RegisterRequest } from '../../models/auth.request.model';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [
    MatIconModule,
    MatInputModule,
    MatButtonModule,
    CommonModule,
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.scss',
})
export class AuthComponent {
  //#region Properties
  activeForm: string = 'login';
  isLoginMode: boolean = true;
  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
    ]),
  });
  signupForm = new FormGroup({
    firstname: new FormControl('', [Validators.required]),
    lastname: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
    phonenumber: new FormControl('', [
      Validators.required,
      Validators.minLength(10),
      Validators.pattern('^[0-9]*$'),
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
    ]),
    confirmPassword: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
    ]),
  });

  signupError: boolean = false;
  loginError: boolean = false;

  //#endregion

  //#region constructor
  authService = inject(AuthService);
  router = inject(Router);
  constructor(private route: ActivatedRoute) {
    route.url.subscribe((url) => {
      let path = url[0].path;
      path = path == 'register' ? 'signup' : path;
      console.log('Path: ', path);
      this.toggleForm(path);
    });
  }
  //#endregion

  toggleForm(formType: string) {
    this.activeForm = formType;
    this.isLoginMode = formType === 'login';
    if (this.isLoginMode) {
      this.router.navigate(['/auth/login']);
    } else if (formType === 'signup') {
      this.router.navigate(['/auth/register']);
    } else if (formType === 'password/reset') {
      this.router.navigate(['auth/password/reset']);
    }
  }

  async onLoginSubmit() {
    console.log('Login mode');
    if (this.loginForm.valid) {
      const loginRequest: LoginRequest = {
        email: this.loginForm.value.email as string,
        password: this.loginForm.value.password as string,
      };
      try {
        const response = await this.authService.login(loginRequest);
        console.log('User logged in successfully', response);
        this.router.navigate(['/home']);
      } catch (error) {
        console.error('Login Error', error);
        this.loginError = true;
      }
    }
  }

  async onSignupSubmit() {
    console.log('Signup mode');
    if (
      this.signupForm.valid &&
      this.signupForm.value.password === this.signupForm.value.confirmPassword
    ) {
      try {
        const request: RegisterRequest = {
          firstname: this.signupForm.value.firstname as string,
          lastname: this.signupForm.value.lastname as string,
          email: this.signupForm.value.email as string,
          phonenumber: this.signupForm.value.phonenumber as string,
          password: this.signupForm.value.password as string,
          confirmPassword: this.signupForm.value.confirmPassword as string,
        };
        const response = await this.authService.register(request);
        console.log('User registered successfully', response);
        if (response) {
          await this.authService.acknowledgeNewUser(response.url);
          console.log('Account confirmed successfully');
          const login = await this.authService.login(request);
          console.log('User logged in successfully', login);
          this.router.navigate(['/categories']);

          //this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
          //this.router.navigate(['/auth/login']);
          //});
        }
      } catch (error) {
        console.error('Registration Error', error);
        this.signupError = true;
      }
    } else {
      console.log('Form is not valid or passwords do not match');
      this.signupError = true;
    }
  }
}
