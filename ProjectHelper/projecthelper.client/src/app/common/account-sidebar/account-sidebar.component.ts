import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SidebarService } from '../../services/sidebar.service';

@Component({
  selector: 'app-account-sidebar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './account-sidebar.component.html',
  styleUrl: './account-sidebar.component.css',
})
export class AccountSidebarComponent {
  constructor(public sidebarService: SidebarService) {}

  get sidebarOpen() {
    return this.sidebarService.isSidebarOpen;
  }

  closeSidebar() {
    this.sidebarService.closeSidebar();
  }
}
