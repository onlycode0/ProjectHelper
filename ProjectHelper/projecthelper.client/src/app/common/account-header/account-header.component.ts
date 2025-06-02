import { Component } from '@angular/core';

@Component({
  selector: 'app-account-header',
  standalone: true,
  template: `
    <header class="account-header">
      <h1>ProjectHelper</h1>
    </header>
  `,
  styles: [`
    .account-header {
      position: fixed;
      top: 0;
      left: 0;
      right: 0;
      height: 80px;
      background-color: var(--accent-color);
      color: white;
      display: flex;
      align-items: center;
      padding: 0 40px;
      box-shadow: 0 2px 4px var(--header-shadow);
      z-index: 1000;
    }

    h1 {
      font-family: 'Roboto';
      font-size: 24px;
      font-weight: 500;
    }
  `]
})
export class AccountHeaderComponent {} 