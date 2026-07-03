import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login';
import { EventListComponent } from './pages/event-list/event-list';
import { EventFormComponent } from './pages/event-form/event-form';

export const routes: Routes = [
  { path: '', redirectTo: 'events', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'events', component: EventListComponent },
  { path: 'events/new', component: EventFormComponent },
  { path: 'events/:id', component: EventFormComponent },
];
