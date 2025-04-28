import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { SidebarService } from '../../services/sidebar.service';

@Component({
  selector: 'app-main-header',
  standalone: false,
  templateUrl: './main-header.component.html',
  styleUrl: './main-header.component.css',
})
export class MainHeaderComponent {
  userEmail: string = '';

  isLoggedIn: boolean = false;

  constructor(
    private authService: AuthService,
    private sidebarService: SidebarService
  ) {}

  ngOnInit() {
    this.userEmail = this.authService.getEmail();
    this.isLoggedIn = this.authService.isAuthenticated(); // узнаём авторизован ли
  }

  toggleMenu() {
    this.sidebarService.toggleSidebar();
  }

  logout() {
    // здесь будет логика выхода
  }
}
