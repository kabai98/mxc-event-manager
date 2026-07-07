import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';
import { EventService } from '../../services/event.service';
import { ActivatedRoute } from '@angular/router';
import { ToastService } from '../../services/toast.service';
@Component({
  selector: 'app-event-form',
  imports: [CommonModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatButtonModule],
  standalone: true,
  templateUrl: './event-form.component.html',
  styleUrl: './event-form.component.scss',
})
export class EventFormComponent implements OnInit {
  private fb = inject(FormBuilder);
  private toast = inject(ToastService);
  private route = inject(ActivatedRoute);
  private eventService = inject(EventService);
  private router = inject(Router);

  eventId: number | null = null;

  eventForm = this.fb.nonNullable.group({
    name: ['', [Validators.required]],

    location: ['', [Validators.required, Validators.maxLength(100)]],

    country: [null as string | null],

    capacity: [null as number | null, [Validators.min(1)]],
  });

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');

    if (id) {
      this.eventId = +id;
      this.eventService.getEventById(this.eventId).subscribe((event) => {
        this.eventForm.patchValue(event);
      });
    }
  }

  onSave() {
    if (!this.eventForm.valid) return;

    const dto = this.eventForm.getRawValue();

    if (this.eventId) {
      const updateDto = {
        ...dto,
        id: this.eventId,
      };

      this.eventService.updateEvent(this.eventId, updateDto).subscribe({
        next: () => {
          this.toast.success('Event successfully updated!');
          this.router.navigate(['/events']);
        },
        error: (err) => {
          this.toast.error('An error occurred while updating the event!');
        },
      });

      return;
    }

    this.eventService.createEvent(dto).subscribe({
      next: () => {
        this.toast.success('Event successfully saved!');
        this.router.navigate(['/events']);
      },
      error: (err) => {
        this.toast.error('An error occurred while saving the event!');
      },
    });
  }

  onCancel() {
    this.router.navigate(['/events']);
  }
}
