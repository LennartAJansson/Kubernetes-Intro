import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { BuildVersionsListComponent } from './components/build-versions-list/build-versions-list.component';
import {
  HttpClientModule,
  provideHttpClient,
  withInterceptorsFromDi,
} from '@angular/common/http';
import { BuildVersionDetailComponent } from './components/build-version-detail/build-version-detail.component';
import { NavMenuComponent } from './common/nav-menu/nav-menu.component';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  imports: [
    RouterOutlet,
    NavMenuComponent,
    BuildVersionsListComponent,
    BuildVersionDetailComponent,
    HttpClientModule,
  ],
  //providers: [provideHttpClient(withInterceptorsFromDi())],
})
export class AppComponent {
  title = 'BuildVersions';
}
