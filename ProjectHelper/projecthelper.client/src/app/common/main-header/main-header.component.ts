import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { SidebarService } from '../../services/sidebar.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-main-header',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './main-header.component.html',
  styleUrl: './main-header.component.css',
})
export class MainHeaderComponent implements OnInit, OnDestroy {
  userEmail: string = '';
  isLoggedIn: boolean = false;
  private authSubscription?: Subscription;

  constructor(
    private authService: AuthService,
    private sidebarService: SidebarService
  ) {}

  ngOnInit() {
    this.userEmail = this.authService.getEmail();
    this.authSubscription = this.authService.isAuthenticated()
      .subscribe(isAuthenticated => {
        this.isLoggedIn = isAuthenticated;
      });
  }

  ngOnDestroy() {
    if (this.authSubscription) {
      this.authSubscription.unsubscribe();
    }
  }

  toggleMenu() {
    this.sidebarService.toggleSidebar();
  }

  logout() {
    this.authService.logout().subscribe(() => {
      // Handle logout success if needed
    });
  }
}
