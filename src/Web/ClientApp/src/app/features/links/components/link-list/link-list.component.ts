import { Component, Inject, InjectionToken, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { AuthService } from '@auth0/auth0-angular';
import {
  API_BASE_URL,
  LinkDto,
  LinksClient,
} from 'src/app/shared/services/api.service';

@Component({
  selector: 'app-link-list',
  templateUrl: './link-list.component.html',
  styleUrls: ['./link-list.component.scss'],
})
export class LinkListComponent implements OnInit {
  pageIndex: number = 1;
  pageSize: number = 10;
  totalCount: number = 0;
  totalPages: number = 0;

  items: Array<LinkDto> = [];
  displayedColumns: string[] = ['originalUrl', 'shortUrlCode', 'actions'];

  loading: boolean = false;
  error: string = '';

  baseUrl?: string;

  constructor(
    private linksService: LinksClient,
    private router: Router,
    private authService: AuthService,
    @Inject(API_BASE_URL) baseUrl?: string
  ) {
    this.baseUrl = baseUrl;
  }

  ngOnInit(): void {
    this.fetch();
  }

  fetch() {
    this.loading = true;
    this.linksService.getLinks(this.pageIndex, this.pageSize).subscribe({
      next: (result) => {
        this.items = result.items;
        this.totalCount = result.totalCount;
        this.totalPages = result.totalPages;
        this.loading = false;
        this.error = '';
      },
      error: (err) => {
        const parsedErr = JSON.parse(err.response);
        this.error = parsedErr.title;
        this.loading = false;
      },
    });
  }

  pageChanged(event: PageEvent) {
    this.pageIndex = event.pageIndex + 1;
    this.fetch();
  }

  onLinkGenerated(event: LinkDto) {
    this.fetch();
  }

  navigateToLink(link: LinkDto) {
    window.open(
      `${this.baseUrl ?? window.location.origin}/short/${link.shortUrlCode}`,
      '_blank'
    );
  }

  goToDetails(link: LinkDto) {
    this.router.navigate(['/links', link.id]);
  }

  logout() {
    this.authService
      .logout({
        logoutParams: {
          returnTo: window.location.origin,
        },
      })
      .subscribe({
        next: (value) => {},
      });
  }
}
