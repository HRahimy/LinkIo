import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LinksRoutingModule } from './links-routing.module';
import { LinkListComponent } from './components/link-list/link-list.component';
import { LinkDetailsComponent } from './components/link-details/link-details.component';


@NgModule({
  declarations: [
    LinkListComponent,
    LinkDetailsComponent
  ],
  imports: [
    CommonModule,
    LinksRoutingModule
  ]
})
export class LinksModule { }
