import { Component, EventEmitter, Inject, Output } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { AuthService } from '@auth0/auth0-angular';
import {
  API_BASE_URL,
  CreateLinkCommand,
  LinkDto,
  PublicLinksClient,
} from 'src/app/shared/services/api.service';
import { urlValidator } from 'src/app/shared/validators/valid-url.validator';
import { Clipboard } from '@angular/cdk/clipboard';

@Component({
  selector: 'app-link-generator',
  templateUrl: './link-generator.component.html',
  styleUrl: './link-generator.component.scss',
})
export class LinkGeneratorComponent {
  urlInput = new FormControl('', [Validators.required, urlValidator()]);

  shortUrl: string;

  loading: boolean = false;
  error: string = '';

  @Output() linkGenerated: EventEmitter<LinkDto> = new EventEmitter();

  private baseUrl?: string;

  constructor(
    private links: PublicLinksClient,
    private authService: AuthService,
    private clipBoard: Clipboard,
    @Inject(API_BASE_URL) baseUrl?: string
  ) {
    this.baseUrl = baseUrl;
  }

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
            this.shortUrl = `${this.baseUrl ?? window.origin}/short/${
              result.shortUrlCode
            }`;
            this.error = '';
            this.loading = false;
            this.urlInput.reset('');
            this.urlInput.enable();
            this.linkGenerated.emit(result);
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

  copyUrl() {
    this.clipBoard.copy(this.shortUrl);
  }
}
