import { Component } from '@angular/core';
import { Router } from '@angular/router';  // Импортируем Router
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-log-in',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css'],
})
export class LogInComponent {

  name: string = '';
  email: string = '';
  password: string = '';

  currentMode: 'login' | 'register' = 'login'; // По умолчанию вход
  registrationComplete: boolean = false; // Инициализация переменной с типом boolean

  constructor(private router: Router, private authService: AuthService) {}

  switchMode(mode: 'login' | 'register') {
    this.currentMode = mode;
    this.registrationComplete = false; // Сброс состояния при смене режима
  }

  completeRegistration(event: Event): void {
    event.preventDefault(); // Предотвращаем отправку формы
    this.registrationComplete = true; // Устанавливаем статус завершенной регистрации
  }

  login() {
    if (this.email && this.password) {
      console.log('Attempting to login with:', this.email);
      
      this.authService.login({
        login: this.email,
        password: this.password
      }).subscribe({
        next: (success) => {
          if (success) {
            console.log('Login successful');
            const userRole = this.authService.getCurrentUserRole();
            console.log('User Role:', userRole);
            this.authService.setEmail(this.email);
            
            // Перенаправляем на соответствующую страницу в зависимости от роли
            if (userRole === 'Developer') {
              this.router.navigate(['/developer-dashboard']);
            } else {
              this.router.navigate(['/account']);
            }
          }
        },
        error: (error) => {
          console.error('Login failed:', error);
          // Здесь можно добавить отображение ошибки пользователю
        }
      });
    }
  }
}
