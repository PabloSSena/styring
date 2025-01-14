import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from '../enviroments/enviroment';
import { Credentials } from '../types/credentials';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  isLoggedIn() {
    throw new Error('Method not implemented.');
  }
  jwtService: JwtHelperService = new JwtHelperService();
  private apiUrl = environment.API_URL;

  constructor(private http: HttpClient) {}

  authenticate(credentials: Credentials) {
    return this.http.post(`${this.apiUrl}/login`, credentials, {
      observe: 'response',
      responseType: 'text',
    });
  }

  isAuthenticated(): boolean {
    if (typeof sessionStorage !== 'undefined') {
      return !!sessionStorage.getItem('auth-token');
    }
    return false;
  }

  successfulLogin(token: string): void {
    if (typeof sessionStorage !== 'undefined') {
      sessionStorage.setItem('token', token);
    }
  }

  logout() {
    sessionStorage.clear();
  }
}
