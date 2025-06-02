import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { environment } from '../../environments/environment';

export interface Company {
  id: string;
  name: string;
}

@Injectable({
  providedIn: 'root'
})
export class CompanyService {
  private readonly apiUrl = environment.apiUrl;
  private currentCompanySubject = new BehaviorSubject<Company | null>(null);

  constructor(private http: HttpClient) {}

  createCompany(name: string): Observable<Company> {
    return this.http.post<Company>(`${this.apiUrl}/api/Companies`, { name });
  }

  getCurrentCompany(): Observable<Company | null> {
    return this.currentCompanySubject.asObservable();
  }

  setCurrentCompany(company: Company | null) {
    this.currentCompanySubject.next(company);
  }

  getCompanyById(id: string): Observable<Company> {
    return this.http.get<Company>(`${this.apiUrl}/api/Companies/${id}`);
  }

  getCompanyByUserId(): Observable<Company> {
    return this.http.get<Company>(`${this.apiUrl}/api/Companies/current`);
  }
} 