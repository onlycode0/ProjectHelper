import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SidebarService {
  private _isSidebarOpen: boolean = false;

  get isSidebarOpen() {
    return this._isSidebarOpen;
  }

  toggleSidebar() {
    this._isSidebarOpen = !this._isSidebarOpen;
  }

  openSidebar() {
    this._isSidebarOpen = true;
  }

  closeSidebar() {
    this._isSidebarOpen = false;
  }
}
