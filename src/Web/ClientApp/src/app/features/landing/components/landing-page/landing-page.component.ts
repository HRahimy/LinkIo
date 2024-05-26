import { Component, OnDestroy, input } from '@angular/core';
import { FormControl } from '@angular/forms';
import { AuthService } from '@auth0/auth0-angular';
import { Subscription } from 'rxjs';
import {
  CreatePublicLinkCommand,
  PublicLinksClient,
} from 'src/app/shared/services/api.service';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.scss'],
})
export class LandingPageComponent implements OnDestroy {
  urlInput = new FormControl('');

  shortUrl: string;

  loading: boolean = false;
  error: string = '';

  inputSub: Subscription;

  constructor(
    private links: PublicLinksClient,
    private authService: AuthService
  ) {
    this.inputSub = this.urlInput.valueChanges.subscribe({
      next: (value) => {
        console.log(value);
      },
    });
  }

  ngOnDestroy(): void {
    if (this.inputSub) {
      this.inputSub.unsubscribe();
    }
  }

  submit() {
    if (this.urlInput.valid) {
      this.loading = true;
      this.links
        .createLink(
          new CreatePublicLinkCommand({
            url: this.urlInput.getRawValue(),
          })
        )
        .subscribe({
          next: (result) => {
            console.log(result);
            this.shortUrl = `${window.origin}/short/${result.shortUrlCode}`;
          },
        });
    }
  }

  signIn() {
    this.authService.loginWithRedirect();
  }
}
