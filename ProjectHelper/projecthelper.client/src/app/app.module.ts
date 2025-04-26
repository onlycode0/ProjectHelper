import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainPageComponent } from './pages/main-page/main-page.component';
import { MainHeaderComponent } from './pages/main-page/main-header/main-header.component';
import { LogInComponent } from './pages/main-page/log-in/log-in.component';


@NgModule({
  declarations: [
    MainPageComponent,
    AppComponent,
    MainHeaderComponent,
    MainHeaderComponent,
    LogInComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
