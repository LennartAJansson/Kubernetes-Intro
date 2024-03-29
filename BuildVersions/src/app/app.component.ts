import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { BuildVersionsListComponent } from './components/build-versions-list/build-versions-list.component';
import { HttpClientModule } from '@angular/common/http';
import { BuildVersionDetailComponent } from './components/build-version-detail/build-version-detail.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    BuildVersionsListComponent,
    HttpClientModule,
    BuildVersionDetailComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'BuildVersions';
}
