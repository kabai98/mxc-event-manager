import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class TokenService {
  private tokenKey = 'token';
  private emailKey = 'email';

  setToken(token: string) {
    localStorage.setItem(this.tokenKey, token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  removeToken() {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.emailKey);
  }

  hasToken(): boolean {
    return !!this.getToken();
  }

  setEmail(email: string) {
    localStorage.setItem(this.emailKey, email);
  }

  getEmail(): string | null {
    return localStorage.getItem(this.emailKey);
  }

  removeEmail() {
    localStorage.removeItem(this.emailKey);
  }
}
