import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private userEmail: string = '';

  setEmail(email: string) {
    this.userEmail = email;
  }

  getEmail(): string {
    return this.userEmail;
  }

  isAuthenticated(): boolean {
    return this.userEmail !== '';
  }
}
