import { Component } from '@angular/core';
import { SidebarService } from '../../services/sidebar.service';

@Component({
  selector: 'app-account-sidebar',
  standalone: false,
  templateUrl: './account-sidebar.component.html',
  styleUrl: './account-sidebar.component.css',
})
export class AccountSidebarComponent {
  constructor(public sidebarService: SidebarService) {}

  get sidebarOpen() {
    return this.sidebarService.isSidebarOpen;
  }
}
