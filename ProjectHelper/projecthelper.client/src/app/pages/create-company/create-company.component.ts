import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CompanyService, Company } from '../../services/company.service';

@Component({
  selector: 'app-create-company',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './create-company.component.html',
  styleUrls: ['./create-company.component.css']
})
export class CreateCompanyComponent {
  companyName: string = '';

  constructor(
    private companyService: CompanyService,
    private router: Router
  ) {}

  createCompany() {
    if (this.companyName) {
      this.companyService.createCompany(this.companyName).subscribe({
        next: (response: Company) => {
          this.router.navigate(['/welcome'], { 
            state: { companyName: this.companyName } 
          });
        },
        error: (error: Error) => {
          console.error('Error creating company:', error);
        }
      });
    }
  }
} 