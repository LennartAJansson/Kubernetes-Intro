import { Injectable, inject } from '@angular/core';
import { CanMatchFn, Router, } from '@angular/router';
import { AuthService } from '../../services/auth.service';

export const AuthGuard: CanMatchFn = async (route, segments) => {
  const authService = inject(AuthService);
  const router = inject(Router);
  console.log('Test Guard');

  if (!authService.isAuthenticated() && !authService.isAdmin()) {

    router.navigate(['/login']);
    return false;
  }
  console.log('User is authenticated & is admin');
  return true;

}

