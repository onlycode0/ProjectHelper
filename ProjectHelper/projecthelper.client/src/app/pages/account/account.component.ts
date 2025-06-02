import { Component } from '@angular/core';
import { AccountSidebarComponent } from '../../common/account-sidebar/account-sidebar.component';
import { MainHeaderComponent } from '../../common/main-header/main-header.component';

@Component({
  selector: 'app-account',
  standalone: true,
  imports: [AccountSidebarComponent, MainHeaderComponent],
  templateUrl: './account.component.html',
  styleUrl: './account.component.css'
})
export class AccountComponent {

}
