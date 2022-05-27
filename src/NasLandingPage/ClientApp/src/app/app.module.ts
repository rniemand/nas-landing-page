import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './layout/nav-menu/nav-menu.component';
import { HomeComponent } from './views/home/home.component';
import { API_BASE_URL, ConfigClient, ProjectsClient, UserLinksClient } from './nlp-api';
import { DOCUMENT, LocationStrategy } from '@angular/common';
import { ProjectTableComponent } from './components/project-table/project-table.component';
import { RepoTypeComponent } from './components/repo-type/repo-type.component';
import { BoolTickComponent } from './components/bool-tick/bool-tick.component';
import { ArrayStringComponent } from './components/array-string/array-string.component';
import { SqBadgesComponent } from './components/sq-badges/sq-badges.component';
import { ArrayCountComponent } from './components/array-count/array-count.component';
import { OptionEditorComponent } from './components/project-table/option-editor/option-editor.component';
import { LinkSqComponent } from './components/link-sq/link-sq.component';
import { LinkGenericComponent } from './components/link-generic/link-generic.component';
import { RawCountComponent } from './components/raw-count/raw-count.component';
import { HumanSizeComponent } from './components/human-size/human-size.component';
import { ProjectsComponent } from './views/projects/projects.component';
import { UserLinksComponent } from './components/user-links/user-links.component';
import { UserLinkComponent } from './components/user-link/user-link.component';
import { CommandResponseComponent } from './components/command-response/command-response.component';
import { LinkCategoryComponent } from './components/link-category/link-category.component';

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
    ProjectTableComponent,
    RepoTypeComponent,
    BoolTickComponent,
    ArrayStringComponent,
    SqBadgesComponent,
    ArrayCountComponent,
    OptionEditorComponent,
    LinkSqComponent,
    LinkGenericComponent,
    RawCountComponent,
    HumanSizeComponent,
    ProjectsComponent,
    UserLinksComponent,
    UserLinkComponent,
    CommandResponseComponent,
    LinkCategoryComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    
    HttpClientModule,
    FormsModule,
    HttpClientModule,

    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'projects', component: ProjectsComponent },
    ])
  ],
  providers: [
    // Clients
    ProjectsClient,
    ConfigClient,
    UserLinksClient,

    // Custom providers
    { provide: API_BASE_URL, useFactory: getBaseUrl, deps: [LocationStrategy, DOCUMENT] }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
