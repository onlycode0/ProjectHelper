import { Component } from '@angular/core';

@Component({
  selector: 'app-statistics',
  standalone: true,
  template: `
    <div class="statistics-container">
      <h1>Статистика</h1>
      <!-- Здесь будет содержимое страницы статистики -->
    </div>
  `,
  styles: [`
    .statistics-container {
      padding: 20px;
    }
    
    h1 {
      color: var(--dark-color);
      margin-bottom: 20px;
    }
  `]
})
export class StatisticsComponent {} 