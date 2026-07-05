import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatSortModule, MatSort } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { EventViewModel } from '../../models/event';
import { EventService } from '../../services/event';
@Component({
  selector: 'app-event-list',
  imports: [CommonModule, MatTableModule, MatSortModule, MatButtonModule, MatIconModule],
  standalone: true,
  templateUrl: './event-list.html',
  styleUrl: './event-list.scss',
})
export class EventListComponent implements OnInit {
  displayedColumns: string[] = ['name', 'place', 'capacity', 'actions'];
  dataSource = new MatTableDataSource<EventViewModel>([]);
  events: EventViewModel[] = [];
  errorMessage: string = '';

  @ViewChild(MatSort) sort!: MatSort;
  // 1. Injektáljuk az új @Service()-t
  private eventService = inject(EventService);

  ngOnInit() {
    //this.initApp();
    this.loadEvents();
  }

  // Statikus adatok a kép alapján
  // const dummyData: EventViewModel[] = [
  //   { id: 1, name: 'Bebury Park', city: 'Alexandria', country: 'Egypt', capacity: 7720 },
  //   {
  //     id: 2,
  //     name: 'Trughtcote Avenue',
  //     city: 'Caloocan',
  //     country: 'Philippines',
  //     capacity: 19900,
  //   },
  //   { id: 3, name: 'Apathampton Manor.', city: 'Guadalajara', country: 'Mexico', capacity: 5474 },
  //   { id: 4, name: 'Wrecote Lake', city: 'Vijayawada', country: 'India', capacity: 8673 },
  //   { id: 5, name: 'Bunthold', city: 'Mashhad', country: 'Iran', capacity: 19043 },
  //   { id: 6, name: 'Gradstan', city: 'Osaka', country: 'Japan', capacity: 1961 },
  //   { id: 7, name: 'Pustan Way', city: 'Algiers', country: 'Algeria', capacity: 15839 },
  //   { id: 8, name: 'Blithampdale', city: 'Prague', country: 'Czech Republic', capacity: 908 },
  //   { id: 9, name: 'Plodon Park', city: 'Chennai', country: 'India', capacity: 19758 },
  //   {
  //     id: 10,
  //     name: 'Flumore Tye',
  //     city: 'Dubai',
  //     country: 'United Arab Emirates',
  //     capacity: 2246,
  //   },
  // ];

  onAddEvent() {
    console.log('Add new event clicked');
  }

  onEdit(id: number): void {
    console.log('Edit event:', id);
    this.eventService.getEventById(id).subscribe({
      next: (event) => {
        // át kellene navigálni az event from komponenshez az esemény adataival
        console.log('Fetched event for editing:', event);
        // Itt lehetne navigálni az EventFormComponent-hez az esemény adataival
      },
      error: (err) => {
        console.error('Error fetching event:', err);
      },
    });
  }
  onDelete(id: number): void {
    console.log('Delete event:', id);
    this.eventService.deleteEvent(id).subscribe({
      next: () => {
        // Ha a szerverről letörlődött, a képernyőről is kiszűrjük
        this.events = this.events.filter((e) => e.id !== id);
      },
    });
  }

  loadEvents(): void {
    this.eventService.getEvents().subscribe({
      next: (data) => {
        console.log('Fetched events:', data);
        this.dataSource.data = data;
        this.events = data;
      },
      error: (err) => {
        console.error('Error fetching events:', err);
        this.errorMessage = 'Hiba a betöltéskor!';
      },
    });
  }
}
