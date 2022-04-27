import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './layout/nav-menu/nav-menu.component';
import { HomeComponent } from './views/home/home.component';
import { CounterComponent } from './views/counter/counter.component';
import { FetchDataComponent } from './views/fetch-data/fetch-data.component';
import { API_BASE_URL, ProjectsClient } from './nlp-api';
import { DOCUMENT, LocationStrategy } from '@angular/common';

function getBaseUrl(locationStrategy: LocationStrategy, document: any): string {
  let baseHref = locationStrategy.getBaseHref();

  if(baseHref === '/')
    return '';

  let baseUrl = `${document.location.origin}${locationStrategy.getBaseHref()}`;

  if(!baseUrl.endsWith('/')) {
    return baseUrl;
  }

  return baseUrl.substring(0, baseUrl.length -1);
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    
    HttpClientModule,
    FormsModule,
    HttpClientModule,

    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
    ])
  ],
  providers: [
    // Clients
    ProjectsClient,

    // Custom providers
    { provide: API_BASE_URL, useFactory: getBaseUrl, deps: [LocationStrategy, DOCUMENT] }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
