import { Component, OnDestroy, OnInit } from '@angular/core';
import { AngularMaterialComponent } from '../angular-material/angular-material.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { Router, RouterLink } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { AuthService } from '../../services/auth.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-nav-menu',
  standalone: true,
  imports: [
    AngularMaterialComponent,
    MatToolbarModule,
    RouterLink,
    MatIconModule,
    MatButtonModule,
  ],
  templateUrl: './nav-menu.component.html',
  styleUrl: './nav-menu.component.scss',
})
export class NavMenuComponent implements OnInit, OnDestroy {
  isAuthenticated: boolean = false;
  username: string = '';
  private authSubscription!: Subscription;

  constructor(private router: Router, private authService: AuthService) {}

  ngOnInit() {
    this.authSubscription = this.authService.currentUserSubject.subscribe(
      (user) => {
        if (user) {
          console.log('Current user data in subscribtion:', user);
          this.username = this.authService.getUserName()!;
        }
        this.isAuthenticated = !!user;
      }
    );
  }

  ngOnDestroy(): void {
    this.authSubscription.unsubscribe();
  }

  login() {
    this.router.navigate(['/auth/login']);
  }

  logout() {
    this.authService
      .logout()
      .then(() => {
        this.router.navigate(['/']);
      })
      .catch((error) => {
        console.error('Logout failed:', error);
      });
  }
}
