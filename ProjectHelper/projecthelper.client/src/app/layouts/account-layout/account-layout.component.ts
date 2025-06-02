import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AccountSidebarComponent } from '../../common/account-sidebar/account-sidebar.component';
import { MainHeaderComponent } from '../../common/main-header/main-header.component';

@Component({
  selector: 'app-account-layout',
  standalone: true,
  imports: [RouterOutlet, MainHeaderComponent, AccountSidebarComponent],
  template: `
    <app-main-header></app-main-header>
    <app-account-sidebar></app-account-sidebar>
    <main class="content">
      <router-outlet></router-outlet>
    </main>
  `,
  styles: [`
    .content {
      margin-left: 250px;
      margin-top: 120px;
      padding: 20px;
    }
  `]
})
export class AccountLayoutComponent {} 