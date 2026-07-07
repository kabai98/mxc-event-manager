import { Injectable, inject } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({ providedIn: 'root' })

// Provides reusable toast notifications across the application
export class ToastService {
  private snackBar = inject(MatSnackBar);

  success(message: string) {
    this.snackBar.open(message, 'OK', {
      duration: 3000,
      panelClass: ['toast-success'],
    });
  }

  error(message: string) {
    this.snackBar.open(message, 'Bezár', {
      duration: 4000,
      panelClass: ['toast-error'],
    });
  }

  info(message: string) {
    this.snackBar.open(message, 'OK', {
      duration: 3000,
      panelClass: ['toast-info'],
    });
  }
}
