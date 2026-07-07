import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatSortModule, MatSort } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { EventService } from '../../services/event.service';
import { Router } from '@angular/router';
import { EventViewModel } from '../../models/event.view-model';
import { ToastService } from '../../services/toast.service';
@Component({
  selector: 'app-event-list',
  imports: [CommonModule, MatTableModule, MatSortModule, MatButtonModule, MatIconModule],
  standalone: true,
  templateUrl: './event-list.component.html',
  styleUrl: './event-list.component.scss',
})
export class EventListComponent implements OnInit {
  @ViewChild(MatSort) sort!: MatSort;

  displayedColumns: string[] = ['name', 'location', 'capacity', 'actions'];
  dataSource = new MatTableDataSource<EventViewModel>([]);

  private toast = inject(ToastService);
  private eventService = inject(EventService);
  private router = inject(Router);

  ngOnInit() {
    this.loadEvents();
  }

  onAddEvent() {
    this.router.navigate(['/events/new']);
  }

  onEdit(id: number): void {
    this.router.navigate(['/events', id]);
  }

  onDelete(id: number): void {
    const oldEvents = [...this.dataSource.data];

    this.dataSource.data = this.dataSource.data.filter((e) => e.id !== id);

    this.eventService.deleteEvent(id).subscribe({
         next: () => {
        this.toast.success('Event deleted successfully!');
      },
      error: () => {
        this.toast.error('Error while deleting the event!');
        this.dataSource.data = oldEvents;
      },
    });
  }

  loadEvents(): void {
    this.eventService.getEvents().subscribe({
      next: (data) => {
        this.dataSource.data = data;
        this.dataSource.sort = this.sort;
      },
      error: (err) => {
        this.toast.error('Error fetching events!');
      },
    });
  }
}
