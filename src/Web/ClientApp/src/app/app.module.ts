import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import {
  HttpClientModule,
  provideHttpClient,
  withInterceptors,
} from '@angular/common/http';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideAuth0 } from '@auth0/auth0-angular';
import { authInterceptor } from './shared/interceptors/auth.interceptor';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { API_BASE_URL } from './shared/services/api.service';
import { environment } from 'src/environments/environment';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {
        path: '',
        loadChildren: () =>
          import('./layout/layout.module').then((m) => m.LayoutModule),
      },
    ]),
    BrowserAnimationsModule,
    MatProgressSpinnerModule,
  ],
  providers: [
    provideAuth0({
      domain: 'ngbridge.eu.auth0.com',
      clientId: '5YkZLVGjzit5ReUsIcCURgE6uvP7hTCm',
      authorizationParams: {
        redirect_uri: window.location.origin,
        audience: 'https://linkio.com',
        scope: 'read:current_user',
      },
    }),
    provideHttpClient(withInterceptors([authInterceptor])),
    {
      provide: API_BASE_URL,
      useValue: environment.apiUrl,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
