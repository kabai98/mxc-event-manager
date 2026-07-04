import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
@Component({
  selector: 'app-event-form',
  imports: [CommonModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatButtonModule],
  standalone: true,
  templateUrl: './event-form.html',
  styleUrl: './event-form.scss',
})
export class EventFormComponent implements OnInit {
  eventForm: FormGroup;

  constructor(private fb: FormBuilder) {
    // Az űrlap definíciója a képen látható alapértelmezett adatokkal
    this.eventForm = this.fb.group({
      name: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      capacity: ['', [Validators.required, Validators.pattern('^[0-9]*$')]],
    });
  }

  ngOnInit(): void {}

  onSave() {
    if (this.eventForm.valid) {
      console.log('Saved data:', this.eventForm.value);
    }
  }

  onCancel() {
    console.log('Editing cancelled');
    // Itt általában visszanavigálunk a listához
  }
}
