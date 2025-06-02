import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MainPageComponent } from './pages/main-page/main-page.component';
import { MainHeaderComponent } from './common/main-header/main-header.component';
import { LogInComponent } from './pages/main-page/log-in/log-in.component';
import { AccountComponent } from './pages/account/account.component';
import { AccountSidebarComponent } from './common/account-sidebar/account-sidebar.component';
import { AuthInterceptor } from './services/auth.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    MainPageComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    MainHeaderComponent,
    AccountSidebarComponent,
    LogInComponent
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
