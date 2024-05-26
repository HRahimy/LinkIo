import { Component, OnDestroy, input } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { AuthService } from '@auth0/auth0-angular';
import { Subscription } from 'rxjs';
import {
  CreateLinkCommand,
  PublicLinksClient,
} from 'src/app/shared/services/api.service';
import { Clipboard } from '@angular/cdk/clipboard';
import { urlValidator } from 'src/app/shared/validators/valid-url.validator';

@Component({
  selector: 'app-landing-page',
  templateUrl: './landing-page.component.html',
  styleUrls: ['./landing-page.component.scss'],
})
export class LandingPageComponent {
  urlInput = new FormControl('', [Validators.required, urlValidator()]);

  shortUrl: string;

  loading: boolean = false;
  error: string = '';

  constructor(
    private links: PublicLinksClient,
    private authService: AuthService,
    private clipBoard: Clipboard
  ) {}

  submit() {
    if (this.urlInput.valid) {
      this.loading = true;
      this.urlInput.disable();
      this.links
        .createPublicLink(
          new CreateLinkCommand({
            url: this.urlInput.getRawValue(),
          })
        )
        .subscribe({
          next: (result) => {
            this.shortUrl = `${window.origin}/short/${result.shortUrlCode}`;
            this.error = '';
            this.loading = false;
            this.urlInput.reset('');
            this.urlInput.enable();
          },
          error: (err) => {
            const parsedErr = JSON.parse(err.response);
            this.error = parsedErr.title;
            this.loading = false;
            this.urlInput.enable();
          },
        });
    }
  }

  signIn() {
    this.authService.loginWithRedirect();
  }

  copyUrl() {
    this.clipBoard.copy(this.shortUrl);
  }
}
