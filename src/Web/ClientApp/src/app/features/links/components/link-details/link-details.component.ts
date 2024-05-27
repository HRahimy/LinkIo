import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
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

  mode: 'view' | 'edit' = 'view';

  link: LinkDetailsDto;

  loading: boolean = false;
  error: string = '';

  displayedColumns: string[] = ['url', 'count'];

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

  fetch() {
    this.loading = true;
    this.linksService.getLinkDetails(+this.id).subscribe({
      next: (result) => {
        this.link = result;
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

  goBack() {
    this.router.navigate(['/links']);
  }
}
