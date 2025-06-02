import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '../../environments/environment';

// These should match exactly with the C# enum values
export enum DeveloperSkills {
  React = 0,
  CSharp = 1,
  CSS = 2,
  HTML = 3,
  Angular = 4,
  MongoDB = 5
}

export enum SkillsLevel {
  Easy = 0,
  Medium = 1,
  Complicated = 2,
  Convoluted = 3,
  Hard = 4
}

// Helper function to get enum name by value
export function getEnumKeyByValue<T extends { [key: string]: any }>(enumObj: T, value: any): keyof T | undefined {
  const keys = Object.keys(enumObj) as Array<keyof T>;
  return keys.find(key => enumObj[key] === value);
}

export interface Developer {
  id: string;
  name: string;
  login: string;
  password: string;
  skills: { [key: string]: number };
  experience: number;
  dailyCapacity: number;
  registrationDate: Date;
  companyId: string;
  projectIds: string[];
  schedule: { [key: string]: number };
}

export interface DeveloperData {
  id?: string;
  name: string;
  login: string;
  password: string;
  skills: { [key: string]: number };
  experience: number;
  dailyCapacity: number;
  registrationDate: Date;
  companyId: string | null;
  projectIds: string[];
  schedule: { [key: string]: number };
}

export type CreateDeveloperRequest = DeveloperData;

@Injectable({
  providedIn: 'root'
})
export class DeveloperService {
  private readonly apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');
    return new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    });
  }

  private handleError(error: HttpErrorResponse) {
    console.error('An error occurred:', error);
    if (error.error instanceof ErrorEvent) {
      // Клиентская или сетевая ошибка
      console.error('Client-side error:', error.error.message);
    } else {
      // Серверная ошибка
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${JSON.stringify(error.error)}`);
    }
    return throwError(() => error);
  }

  createDeveloper(request: CreateDeveloperRequest): Observable<Developer> {
    const formattedDeveloper = {
      ...request,
      id: request.id || '',
      companyId: request.companyId || '',
      skills: Object.entries(request.skills).reduce((acc, [skillName, level]) => {
        acc[skillName] = level.toString();
        return acc;
      }, {} as { [key: string]: string })
    };
    
    console.log('Данные разработчика:', formattedDeveloper);
    
    return this.http.post<Developer>(`${this.apiUrl}/api/Developers`, formattedDeveloper, {
      headers: this.getHeaders()
    }).pipe(
      catchError(this.handleError)
    );
  }

  updateDeveloper(request: Developer): Observable<Developer> {
    const formattedDeveloper = {
      ...request,
      skills: Object.entries(request.skills).reduce((acc, [skillName, level]) => {
        acc[skillName] = level.toString();
        return acc;
      }, {} as { [key: string]: string })
    };
    
    return this.http.put<Developer>(`${this.apiUrl}/api/Developers/${request.id}`, formattedDeveloper, {
      headers: this.getHeaders()
    }).pipe(
      catchError(this.handleError)
    );
  }

  getDevelopers(): Observable<Developer[]> {
    return this.http.get<Developer[]>(`${this.apiUrl}/api/Developers`, {
      headers: this.getHeaders()
    }).pipe(
      catchError(this.handleError)
    );
  }
} 