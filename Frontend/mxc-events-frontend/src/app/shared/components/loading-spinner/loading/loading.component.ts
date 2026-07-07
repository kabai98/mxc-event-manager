import { Component, inject } from '@angular/core';
import { LoadingService } from '../../../../services/loading.service';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-loading',
  imports: [AsyncPipe, MatProgressSpinnerModule],
  template: `
    @if (loading$ | async) {
      <div class="overlay">
        <mat-spinner diameter="60"></mat-spinner>
      </div>
    }
  `,
  styleUrl: './loading.component.scss',
})
export class Loading {
  private loadingService = inject(LoadingService);

  loading$ = this.loadingService.loading$;
}
