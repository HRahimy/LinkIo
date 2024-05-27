import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LinksRoutingModule } from './links-routing.module';
import { LinkListComponent } from './components/link-list/link-list.component';
import { LinkDetailsComponent } from './components/link-details/link-details.component';
import { LinkGeneratorModule } from '../link-generator/link-generator.module';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatDividerModule } from '@angular/material/divider';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@NgModule({
  declarations: [LinkListComponent, LinkDetailsComponent],
  imports: [
    CommonModule,
    LinksRoutingModule,
    LinkGeneratorModule,
    MatToolbarModule,
    MatDividerModule,
    MatPaginatorModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    ReactiveFormsModule,
    MatListModule,
    MatInputModule,
    MatProgressSpinnerModule,
  ],
})
export class LinksModule {}
