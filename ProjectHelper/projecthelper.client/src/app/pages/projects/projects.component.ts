import { Component } from '@angular/core';

@Component({
  selector: 'app-projects',
  standalone: true,
  template: `
    <div class="projects-container">
      <h1>Проекты</h1>
      <!-- Здесь будет содержимое страницы проектов -->
    </div>
  `,
  styles: [`
    .projects-container {
      padding: 20px;
    }
    
    h1 {
      color: var(--dark-color);
      margin-bottom: 20px;
    }
  `]
})
export class ProjectsComponent {} 