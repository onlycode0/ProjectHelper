import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-log-in',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css'],
})
export class LogInComponent {
  currentMode: 'login' | 'register' = 'login'; // По умолчанию вход
  registrationComplete: boolean = false; // Инициализация переменной с типом boolean

  switchMode(mode: 'login' | 'register') {
    this.currentMode = mode;
    this.registrationComplete = false; // Сброс состояния при смене режима
  }

  completeRegistration(event: Event): void {
    event.preventDefault(); // Предотвращаем отправку формы
    this.registrationComplete = true; // Устанавливаем статус завершенной регистрации
  }
}
