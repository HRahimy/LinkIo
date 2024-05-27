import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { ClipboardModule } from '@angular/cdk/clipboard';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { LinkGeneratorComponent } from './components/link-generator/link-generator.component';

@NgModule({
  declarations: [LinkGeneratorComponent],
  exports: [LinkGeneratorComponent],
  imports: [
    CommonModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    ClipboardModule,
    MatProgressSpinnerModule,
    ReactiveFormsModule,
  ],
})
export class LinkGeneratorModule {}
