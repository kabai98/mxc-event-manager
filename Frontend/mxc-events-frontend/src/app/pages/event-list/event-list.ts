import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatSortModule, MatSort } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

export interface EventElement {
  id: number;
  name: string;
  city: string;
  country: string;
  capacity: number;
}
@Component({
  selector: 'app-event-list',
  imports: [CommonModule, MatTableModule, MatSortModule, MatButtonModule, MatIconModule],
  standalone: true,
  templateUrl: './event-list.html',
  styleUrl: './event-list.scss',
})
export class EventListComponent implements OnInit {
  displayedColumns: string[] = ['name', 'place', 'capacity', 'actions'];
  dataSource = new MatTableDataSource<EventElement>([]);

  @ViewChild(MatSort) sort!: MatSort;

  ngOnInit() {
    // Statikus adatok a kép alapján
    const dummyData: EventElement[] = [
      { id: 1, name: 'Bebury Park', city: 'Alexandria', country: 'Egypt', capacity: 7720 },
      {
        id: 2,
        name: 'Trughtcote Avenue',
        city: 'Caloocan',
        country: 'Philippines',
        capacity: 19900,
      },
      { id: 3, name: 'Apathampton Manor.', city: 'Guadalajara', country: 'Mexico', capacity: 5474 },
      { id: 4, name: 'Wrecote Lake', city: 'Vijayawada', country: 'India', capacity: 8673 },
      { id: 5, name: 'Bunthold', city: 'Mashhad', country: 'Iran', capacity: 19043 },
      { id: 6, name: 'Gradstan', city: 'Osaka', country: 'Japan', capacity: 1961 },
      { id: 7, name: 'Pustan Way', city: 'Algiers', country: 'Algeria', capacity: 15839 },
      { id: 8, name: 'Blithampdale', city: 'Prague', country: 'Czech Republic', capacity: 908 },
      { id: 9, name: 'Plodon Park', city: 'Chennai', country: 'India', capacity: 19758 },
      {
        id: 10,
        name: 'Flumore Tye',
        city: 'Dubai',
        country: 'United Arab Emirates',
        capacity: 2246,
      },
    ];

    this.dataSource.data = dummyData;

    // Egyedi rendezési logika a "place" (város + ország) oszlophoz
    this.dataSource.sortingDataAccessor = (item, property) => {
      switch (property) {
        case 'place':
          return item.city.toLowerCase();
        default:
          return (item as any)[property];
      }
    };
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  }

  onAddEvent() {
    console.log('Add new event clicked');
  }

  onEdit(event: EventElement) {
    console.log('Edit event:', event);
  }

  onDelete(event: EventElement) {
    console.log('Delete event:', event);
  }
}