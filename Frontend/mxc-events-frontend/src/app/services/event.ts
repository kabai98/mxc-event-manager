import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { EventViewModel } from '../models/event';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class EventService {
  //ezeket máshova kellene szerintem lehet valami env változo

  private baseUrl = 'https://localhost:7175/api/events';
  private http = inject(HttpClient);

  /// kellene localstorage service és egy auth service,
  // hogy a token-t be tudjuk rakni a headerbe minden requesthez
  /// Get all events
  getEvents(): Observable<EventViewModel[]> {
    return this.http.get<EventViewModel[]>(`${this.baseUrl}`);
  }

  /// Get a specific event by its ID
  getEventById(id: number): Observable<EventViewModel> {
    return this.http.get<EventViewModel>(`${this.baseUrl}/${id}`);
  }

  /// Create a new event
  createEvent(event: EventViewModel): Observable<EventViewModel> {
    return this.http.post<EventViewModel>(this.baseUrl, event);
  }

  /// Update an existing event
  updateEvent(id: number, event: EventViewModel): Observable<EventViewModel> {
    return this.http.put<EventViewModel>(`${this.baseUrl}/${id}`, event);
  }

  /// Delete an existing event
  deleteEvent(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
