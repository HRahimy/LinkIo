import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-link-details',
  templateUrl: './link-details.component.html',
  styleUrls: ['./link-details.component.scss'],
})
export class LinkDetailsComponent implements OnInit {
  id: string;
  constructor(private route: ActivatedRoute) {}

  ngOnInit() {
    this.route.paramMap.subscribe((params) => {
      this.id = params.get('id');
    });
  }
}
