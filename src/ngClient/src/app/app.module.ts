import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// Custom modules
import { MaterialModule } from './modules/material.module';
import { FlexLayoutModule } from '@angular/flex-layout';

// Custom components
import { SideNavComponent } from './modules/core/components/side-nav/side-nav.component';
import { HeaderComponent } from './modules/core/components/header/header.component';

// Custom views
import { HomeComponent } from './modules/core/views/home/home.component';
import { LoginComponent } from './modules/core/views/login/login.component';

@NgModule({
  declarations: [
    AppComponent,

    // Custom components
    SideNavComponent,
    HeaderComponent,
    
    // Custom views
    HomeComponent,
    LoginComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,

    MaterialModule,
    FlexLayoutModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
