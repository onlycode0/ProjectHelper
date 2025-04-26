import { Component } from '@angular/core';

@Component({
  selector: 'app-log-in',
  standalone: false,
  templateUrl: './log-in.component.html',
  styleUrl: './log-in.component.css',
})
export class LogInComponent {
  currentMode: 'login' | 'register' = 'login'; // По умолчанию вход

  switchMode(mode: 'login' | 'register') {
    this.currentMode = mode;
  }
}
