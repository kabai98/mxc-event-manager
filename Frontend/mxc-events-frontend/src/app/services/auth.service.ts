import { HttpClient } from '@angular/common/http';
import { computed, inject, Injectable, signal } from '@angular/core';
import { IdentityResponse } from '../models/identity-response.';
import { TokenService } from './token.service';
import { tap } from 'rxjs/internal/operators/tap';
import { environment } from '../../../environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  // Base URL for authentication-related API endpoints
  private readonly baseUrl = `${environment.apiUrl}/auth`;
  private http = inject(HttpClient);
  private tokenService = inject(TokenService);

  // Stores the current authentication token as a reactive signal
  private readonly token = signal<string | null>(this.tokenService.getToken());

  // Gets the logged-in user's email from storage
  email = computed(() => this.tokenService.getEmail());

  // Checks if the user is currently logged in
  readonly isLoggedIn = computed(() => !!this.token());

  // Sends login request and saves the received token
  login(email: string, password: string) {
    const loginData = { email, password };

    return this.http.post<IdentityResponse>(`${this.baseUrl}/login`, loginData).pipe(
      tap((res) => {
        this.tokenService.setToken(res.accessToken);
        this.tokenService.setEmail(email);

        // Update reactive token state
        this.token.set(res.accessToken);
      }),
    );
  }

  // Logs out the user and removes stored authentication data
  logout() {
    this.tokenService.removeToken();
    this.token.set(null);
  }
}
