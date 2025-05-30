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
    // Здесь можно добавить проверку email и пароля
    if (this.email && this.password) {
      console.log('Вход с email:', this.email, 'и паролем:', this.password);
      this.authService.setEmail(this.email); // Сохраняем email через сервис
      this.router.navigate(['/account']); // Замените '/account' на нужный путь
    } else {
      console.log('Ошибка при входе');
    }
  }
}
