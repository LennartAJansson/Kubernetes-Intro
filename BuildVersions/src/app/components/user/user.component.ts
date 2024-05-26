import { Component, inject } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [MatIcon],
  templateUrl: './user.component.html',
  styleUrl: './user.component.scss',
})
export class UserComponent {
  title = 'Edit User Details';
  router = inject(Router);

  goBack(): void {
    this.router.navigate(['categories']);
  }
}
