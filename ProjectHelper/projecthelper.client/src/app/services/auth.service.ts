import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';

interface TokenResponse {
  accessToken: string;
  refreshToken: string;
}

interface LoginModel {
  login: string;
  password: string;
}

interface DecodedToken {
  name: string;
  role: string;
  exp: number;
  iss: string;
  aud: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly TOKEN_KEY = 'token';
  private readonly REFRESH_TOKEN_KEY = 'refresh_token';
  private readonly EMAIL_KEY = 'user_email';
  private readonly apiUrl = environment.apiUrl;
  private isAuthenticatedSubject = new BehaviorSubject<boolean>(this.hasValidToken());
  private userRoleSubject = new BehaviorSubject<string>('');

  constructor(private http: HttpClient) {
    // Инициализируем роль пользователя при создании сервиса
    const token = this.getToken();
    if (token) {
      const decoded = this.decodeToken(token);
      if (decoded) {
        this.userRoleSubject.next(decoded.role);
      }
    }
  }

  login(credentials: LoginModel): Observable<boolean> {
    console.log('Attempting login with credentials:', { login: credentials.login });
    return this.http.post<TokenResponse>(`${this.apiUrl}/api/Auth/login`, credentials)
      .pipe(
        tap(response => {
          console.log('Login response received:', response);
          this.setTokens(response.accessToken, response.refreshToken);
          this.isAuthenticatedSubject.next(true);
          
          // Устанавливаем роль пользователя
          const decoded = this.decodeToken(response.accessToken);
          if (decoded) {
            this.userRoleSubject.next(decoded.role);
          }
        }),
        map(() => true),
        catchError(error => {
          console.error('Login failed:', error);
          console.error('Error details:', {
            status: error.status,
            message: error.message,
            error: error.error
          });
          return throwError(() => error);
        })
      );
  }

  logout(): Observable<any> {
    return this.http.post(`${this.apiUrl}/api/Auth/logout`, {}).pipe(
      tap(() => {
        this.clearTokens();
        this.isAuthenticatedSubject.next(false);
        this.userRoleSubject.next('');
      })
    );
  }

  refreshToken(): Observable<boolean> {
    const refreshToken = localStorage.getItem(this.REFRESH_TOKEN_KEY);
    if (!refreshToken) {
      return throwError(() => new Error('No refresh token available'));
    }

    return this.http.post<TokenResponse>(`${this.apiUrl}/api/Auth/refresh`, { refreshToken })
      .pipe(
        tap(response => {
          this.setTokens(response.accessToken, response.refreshToken);
        }),
        map(() => true),
        catchError(error => {
          this.clearTokens();
          this.isAuthenticatedSubject.next(false);
          return throwError(() => error);
        })
      );
  }

  isAuthenticated(): Observable<boolean> {
    return this.isAuthenticatedSubject.asObservable();
  }

  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  getEmail(): string {
    return localStorage.getItem(this.EMAIL_KEY) || '';
  }

  setEmail(email: string): void {
    localStorage.setItem(this.EMAIL_KEY, email);
  }

  getUserRole(): Observable<string> {
    return this.userRoleSubject.asObservable();
  }

  getCurrentUserRole(): string {
    return this.userRoleSubject.value;
  }

  private decodeToken(token: string): DecodedToken | null {
    try {
      const decoded = JSON.parse(atob(token.split('.')[1]));
      return {
        name: decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
        role: decoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],
        exp: decoded.exp,
        iss: decoded.iss,
        aud: decoded.aud
      };
    } catch (error) {
      console.error('Error decoding token:', error);
      return null;
    }
  }

  private setTokens(accessToken: string, refreshToken: string): void {
    localStorage.setItem(this.TOKEN_KEY, accessToken);
    localStorage.setItem(this.REFRESH_TOKEN_KEY, refreshToken);
  }

  private clearTokens(): void {
    localStorage.removeItem(this.TOKEN_KEY);
    localStorage.removeItem(this.REFRESH_TOKEN_KEY);
    localStorage.removeItem(this.EMAIL_KEY);
  }

  private hasValidToken(): boolean {
    const token = this.getToken();
    if (!token) return false;

    try {
      const tokenData = JSON.parse(atob(token.split('.')[1]));
      const expirationDate = new Date(tokenData.exp * 1000);
      return expirationDate > new Date();
    } catch {
      return false;
    }
  }
}
