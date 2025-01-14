import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { tap } from 'rxjs';
import { environment } from '../enviroments/enviroment';
import { LoginResponse } from '../types/login-reponse.type';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  private apiUrl = environment.API_URL;
  constructor(private httpClient: HttpClient) {}

  login(name: string, password: string) {
    return this.httpClient
      .post<LoginResponse>(`${this.apiUrl}/auth/login`, { name, password })
      .pipe(
        tap((value) => {
          sessionStorage.setItem('auth-token', value.token);
          sessionStorage.setItem('user-id', value.userId);
        })
      );
  }

  create(name: string, password: string) {
    return this.httpClient.post<LoginResponse>(`${this.apiUrl}/users`, {
      name,
      password,
    });
  }
}
