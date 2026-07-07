import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { EventViewModel } from '../models/event.view-model';
import { EventDto } from '../models/event.dto';
import { Observable } from 'rxjs';
import { environment } from '../../../environment';

@Injectable({
  providedIn: 'root',
})

// Handles API communication for event-related operations
export class EventService {
  // Base URL for event-related API endpoints
  private readonly baseUrl = `${environment.apiUrl}/events`;
  private http = inject(HttpClient);

  getEvents(): Observable<EventViewModel[]> {
    return this.http.get<EventViewModel[]>(this.baseUrl);
  }

  getEventById(id: number): Observable<EventViewModel> {
    return this.http.get<EventViewModel>(`${this.baseUrl}/${id}`);
  }

  createEvent(event: EventDto): Observable<EventDto> {
    return this.http.post<EventDto>(this.baseUrl, event);
  }

  updateEvent(id: number, event: EventDto): Observable<EventDto> {
    return this.http.put<EventDto>(`${this.baseUrl}/${id}`, event);
  }

  deleteEvent(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
