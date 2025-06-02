import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { trigger, state, style, animate, transition } from '@angular/animations';

@Component({
  selector: 'app-welcome',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.css'],
  animations: [
    trigger('fadeOut', [
      state('visible', style({
        opacity: 1
      })),
      state('hidden', style({
        opacity: 0
      })),
      transition('visible => hidden', [
        animate('3s ease-out')
      ])
    ])
  ]
})
export class WelcomeComponent implements OnInit {
  companyName: string = '';
  fadeState: 'visible' | 'hidden' = 'visible';

  constructor(private router: Router) {
    const navigation = this.router.getCurrentNavigation();
    if (navigation?.extras.state) {
      this.companyName = (navigation.extras.state as any).companyName;
    }
  }

  ngOnInit() {
    // Запускаем анимацию исчезновения
    setTimeout(() => {
      this.fadeState = 'hidden';
    }, 100);

    // Перенаправляем на страницу аккаунта после анимации
    setTimeout(() => {
      this.router.navigate(['/account']);
    }, 3000);
  }
} 