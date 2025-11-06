import { Component, inject, OnInit, signal, WritableSignal } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { ToolbarModule } from 'primeng/toolbar';
import { ClinicalCareService } from '../../../core/services/clinical-care.service';
import { forkJoin, switchMap, timer } from 'rxjs';
import { MessageService } from 'primeng/api';

export interface QueueSummary {
  awaitingTriageCount: number;
  awaitingCareCount: number;
}

@Component({
  selector: 'app-toolbar',
  imports: [ToolbarModule, ButtonModule],
  templateUrl: './toolbar.html',
  styleUrl: './toolbar.scss',
})
export class Toolbar implements OnInit {
  private careService = inject(ClinicalCareService);
  private messageService = inject(MessageService);

  public queueSummary: WritableSignal<QueueSummary> = signal({ awaitingTriageCount: 0, awaitingCareCount: 0 });

  public currentTriageNumber: number = 0;

  protected loadQueueData(): void {
    forkJoin({
      summary: this.careService.getQueueSummary(),
      nextSequential: this.careService.getNextSequential()
    }).subscribe({
      next: ({ summary, nextSequential }) => {
        this.queueSummary.set(summary ?? { awaitingTriageCount: 0, awaitingCareCount: 0 });
        this.currentTriageNumber = nextSequential ?? 0;
      },
      error: () => {
        this.messageService.add({
          severity: 'error',
          summary: 'Erro',
          detail: 'Erro ao realizar a consulta da fila de atendimento.'
        });
      }
    });
  }

  public ngOnInit(): void {
    timer(0, 50000).subscribe(() => this.loadQueueData());
  }

  public navigateTo(path: string): void {
    window.open(path, '_blank');
  }
}
