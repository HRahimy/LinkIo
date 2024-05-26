import { Component } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-link-list',
  templateUrl: './link-list.component.html',
  styleUrls: ['./link-list.component.scss'],
})
export class LinkListComponent {
  constructor(private authService: AuthService) {}
  logout() {
    this.authService.logout().subscribe({
      next: () => {
        console.log('logged out');
      },
    });
  }
}
