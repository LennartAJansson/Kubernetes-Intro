import { Routes } from '@angular/router';
import { BuildVersionsListComponent } from './components/build-versions-list/build-versions-list.component';
import { BuildVersionDetailComponent } from './components/build-version-detail/build-version-detail.component';
import { BuildVersionAddComponent } from './components/build-version-add/build-version-add.component';
import { UserComponent } from './components/user/user.component';
import { AuthComponent } from './components/auth/auth.component';
import { ResetPasswordComponent } from './components/reset-password/reset-password.component';

export const routes: Routes = [
  { path: '', redirectTo: 'buildversions', pathMatch: 'full' },
  { path: 'buildversions', component: BuildVersionsListComponent },
  { path: 'buildversion', component: BuildVersionAddComponent },
  { path: 'buildversion/:id', component: BuildVersionDetailComponent },
  { path: 'user', component: UserComponent },
  {
    path: 'auth',
    children: [
      { path: 'login', component: AuthComponent },
      { path: 'register', component: AuthComponent },
      { path: 'password/reset', component: ResetPasswordComponent },
    ],
  },
];
