import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LinkListComponent } from './components/link-list/link-list.component';
import { LinkDetailsComponent } from './components/link-details/link-details.component';
import { authGuard } from 'src/app/shared/guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: LinkListComponent,
  },
  {
    path: ':id',
    component: LinkDetailsComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class LinksRoutingModule {}
