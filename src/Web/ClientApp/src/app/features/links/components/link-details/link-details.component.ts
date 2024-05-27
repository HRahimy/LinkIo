import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import {
  EditLinkCommand,
  LinkDetailsDto,
  LinksClient,
} from 'src/app/shared/services/api.service';

@Component({
  selector: 'app-link-details',
  templateUrl: './link-details.component.html',
  styleUrls: ['./link-details.component.scss'],
})
export class LinkDetailsComponent implements OnInit {
  id: string;

  editing: boolean = false;

  link: LinkDetailsDto;

  loading: boolean = false;
  error: string = '';

  displayedColumns: string[] = ['url', 'count'];

  codeInput = new FormControl('', [
    Validators.minLength(15),
    Validators.maxLength(15),
    Validators.required,
    Validators.pattern('^[a-zA-Z0-9]*$'),
  ]);

  Object = Object;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private linksService: LinksClient
  ) {}

  ngOnInit() {
    this.route.paramMap.subscribe((params) => {
      this.id = params.get('id');
      this.fetch();
    });
  }

  switchMode() {
    if (this.editing) {
      this.codeInput.reset();
    }
    this.editing = !this.editing;
  }

  saveChange() {
    if (this.codeInput.valid) {
      this.loading = true;
      this.codeInput.disable();
      this.linksService
        .editLink(
          +this.id,
          new EditLinkCommand({
            id: +this.id,
            shortUrlCode: this.codeInput.getRawValue(),
          })
        )
        .subscribe({
          next: () => {
            this.loading = false;
            this.error = '';
            this.switchMode();
            this.fetch();
          },
          error: (err) => {
            const parsedErr = JSON.parse(err.response);
            this.error = parsedErr.title;
            this.loading = false;
            this.codeInput.enable();
          },
        });
    }
  }

  fetch() {
    this.loading = true;
    this.linksService.getLinkDetails(+this.id).subscribe({
      next: (result) => {
        this.link = result;
        this.loading = false;
        this.error = '';
        this.codeInput.enable();
      },
      error: (err) => {
        const parsedErr = JSON.parse(err.response);
        this.error = parsedErr.title;
        this.loading = false;
        this.codeInput.enable();
      },
    });
  }

  goBack() {
    this.router.navigate(['/links']);
  }
}
