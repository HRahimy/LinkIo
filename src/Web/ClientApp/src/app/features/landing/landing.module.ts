import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LandingRoutingModule } from './landing-routing.module';
import { LandingPageComponent } from './components/landing-page/landing-page.component';
import { MatDividerModule } from '@angular/material/divider';
import { LinkGeneratorModule } from '../link-generator/link-generator.module';
import { MatButtonModule } from '@angular/material/button';

@NgModule({
  declarations: [LandingPageComponent],
  imports: [
    CommonModule,
    LandingRoutingModule,
    MatDividerModule,
    MatButtonModule,
    LinkGeneratorModule,
  ],
})
export class LandingModule {}
