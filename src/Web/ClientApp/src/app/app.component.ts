import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit, OnDestroy {
  title = 'app';

  authenticatedSub: Subscription;

  loading: boolean = true;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.authenticatedSub = this.authService.isAuthenticated$.subscribe({
      next: (value) => {
        if (value === true) this.router.navigate(['/links']);
        this.loading = false;
      },
    });
  }

  ngOnDestroy(): void {
    if (this.authenticatedSub) {
      this.authenticatedSub.unsubscribe();
    }
  }
}
