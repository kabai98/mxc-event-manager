import { HttpClient } from '@angular/common/http';
import { inject, Service } from '@angular/core';

@Service()
export class User {
  private baseUrl = 'https://localhost:5001/api/users';

  private http = inject(HttpClient);

  login(email: string, password: string) {
    const loginData = { email, password };
    return this.http.post(`${this.baseUrl}/login`, loginData);
  }
}
