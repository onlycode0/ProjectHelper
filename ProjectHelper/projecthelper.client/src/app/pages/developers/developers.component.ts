import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddDeveloperComponent } from './add-developer/add-developer.component';
import { DeveloperService, Developer } from '../../services/developer.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-developers',
  standalone: true,
  imports: [CommonModule, AddDeveloperComponent],
  templateUrl: './developers.component.html',
  styleUrls: ['./developers.component.css']
})
export class DevelopersComponent implements OnInit {
  developers: Developer[] = [];
  showAddForm = false;
  selectedDeveloper: Developer | null = null;

  constructor(private developerService: DeveloperService) {}

  ngOnInit() {
    this.loadDevelopers();
  }

  loadDevelopers() {
    this.developerService.getDevelopers().subscribe({
      next: (developers) => {
        this.developers = developers;
        console.log('Loaded developers:', developers);
      },
      error: (error) => {
        console.error('Error loading developers:', error);
      }
    });
  }

  getSkills(developer: Developer): {name: string, level: string}[] {
    return Object.entries(developer.skills).map(([skillKey, levelKey]) => ({
      name: skillKey,
      level: String(levelKey)
    }));
  }

  onAddDeveloper() {
    this.selectedDeveloper = null;
    this.showAddForm = true;
  }

  onEditDeveloper(developer: Developer) {
    console.log('Edit developer clicked:', developer);
    this.selectedDeveloper = { ...developer };
    this.showAddForm = true;
  }

  onCloseForm() {
    this.showAddForm = false;
    this.selectedDeveloper = null;
  }

  onSaveDeveloper(developer: any) {
    const operation = this.selectedDeveloper 
      ? this.developerService.updateDeveloper(developer)
      : this.developerService.createDeveloper(developer);

    operation.subscribe({
      next: (response) => {
        console.log(this.selectedDeveloper ? 'Developer updated successfully:' : 'Developer saved successfully:', response);
        this.showAddForm = false;
        this.selectedDeveloper = null;
        this.loadDevelopers();
      },
      error: (error: HttpErrorResponse) => {
        console.error(this.selectedDeveloper ? 'Error updating developer:' : 'Error saving developer:', error);
        if (error.error) {
          console.error('Server error message:', error.error);
        }
        if (error.status === 400) {
          console.error('Validation error. Request data:', developer);
        }
      }
    });
  }
} 