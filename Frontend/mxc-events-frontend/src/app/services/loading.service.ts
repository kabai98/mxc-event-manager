import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class LoadingService {
  // Tracks active HTTP requests to control the loading state correctly
  private requestCount = 0;

  // Stores and exposes the current loading state
  private loadingSubject = new BehaviorSubject(false);
  loading$ = this.loadingSubject.asObservable();

  // Starts a loading state when a request begins
  show(): void {
    this.requestCount++;
    this.loadingSubject.next(true);
  }

  // Ends a loading state when a request finishes
  hide(): void {
    this.requestCount--;

    // Stop loading only when all active requests are completed
    if (this.requestCount <= 0) {
      this.requestCount = 0;
      this.loadingSubject.next(false);
    }
  }
}
