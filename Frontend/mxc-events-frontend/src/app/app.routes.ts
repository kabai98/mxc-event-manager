import { Routes } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { EventListComponent } from './pages/event-list/event-list.component';
import { EventFormComponent } from './pages/event-form/event-form.component';
import { authGuard } from './guards/auth-guard';
import { loginGuard } from './guards/login-guard';

export const routes: Routes = [
  { path: '', redirectTo: 'events', pathMatch: 'full' },

  {
    path: 'login',
    component: LoginComponent,
    canActivate: [loginGuard],
  },

  {
    path: 'events',
    canActivate: [authGuard],
    children: [
      { path: '', component: EventListComponent },
      { path: 'new', component: EventFormComponent },
      { path: ':id', component: EventFormComponent },
    ],
  },

  { path: '**', redirectTo: 'events' },
];
