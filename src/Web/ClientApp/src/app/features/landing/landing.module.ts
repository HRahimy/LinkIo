import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LandingRoutingModule } from './landing-routing.module';
import { LandingPageComponent } from './components/landing-page/landing-page.component';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { ClipboardModule } from '@angular/cdk/clipboard';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [LandingPageComponent],
  imports: [
    CommonModule,
    LandingRoutingModule,
    MatInputModule,
    MatButtonModule,
    MatDividerModule,
    MatIconModule,
    ClipboardModule,
    MatProgressSpinnerModule,
    ReactiveFormsModule,
  ],
})
export class LandingModule {}
