import { Routes } from '@angular/router';
import { BuildVersionsListComponent } from './components/build-versions-list/build-versions-list.component';
import { BuildVersionDetailComponent } from './components/build-version-detail/build-version-detail.component';
import { BuildVersionAddComponent } from './components/build-version-add/build-version-add.component';

export const routes: Routes = [
  { path: '', redirectTo: 'buildversions', pathMatch: 'full' },
  { path: 'buildversions', component: BuildVersionsListComponent },
  { path: 'buildversion', component: BuildVersionAddComponent },
  { path: 'buildversion/:id', component: BuildVersionDetailComponent }
];
