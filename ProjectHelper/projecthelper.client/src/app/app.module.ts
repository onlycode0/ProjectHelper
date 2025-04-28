import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainPageComponent } from './pages/main-page/main-page.component';
import { MainHeaderComponent } from './common/main-header/main-header.component';
import { LogInComponent } from './pages/main-page/log-in/log-in.component';
import { AccountComponent } from './pages/account/account.component';
import { AccountSidebarComponent } from './common/account-sidebar/account-sidebar.component';


@NgModule({
  declarations: [
    MainPageComponent,
    AppComponent,
    MainHeaderComponent,
    MainHeaderComponent,
    AccountComponent,
    AccountSidebarComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule,LogInComponent 
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
