import { Component, inject, OnInit, signal } from '@angular/core';
import { Toolbar } from '../../../shared/components/toolbar/toolbar';
import { CommonModule, DatePipe } from '@angular/common';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { TagModule } from 'primeng/tag';
import { DialogService, DynamicDialogModule } from 'primeng/dynamicdialog';
import { TabsModule } from 'primeng/tabs';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';
import { ClinicalCareService } from '../../../core/services/clinical-care.service';
import { TriageService } from '../../../core/services/triage.service';
import { ConfirmationService, MenuItem, MessageService } from 'primeng/api';
import { ClinicalCareDetails } from '../../../core/models/clinical-care.model';
import { TriageFormComponent } from '../components/triage-form.component/triage-form.component';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-triage.component',
  imports: [
    Toolbar,
    CommonModule,
    DatePipe,
    TabsModule,
    TableModule,
    ButtonModule,
    CardModule,
    BreadcrumbModule,
    TableModule,
    TagModule,
    ToastModule,
    ConfirmDialogModule,
    DynamicDialogModule
  ],
  templateUrl: './triage.component.html',
  styleUrl: './triage.component.scss',
})
export class TriageComponent implements OnInit {

  private careService = inject(ClinicalCareService);
  private triageService = inject(TriageService);
  private dialogService = inject(DialogService);
  private messageService = inject(MessageService);
  private confirmationService = inject(ConfirmationService);

  public awaitingTriage = signal<ClinicalCareDetails[]>([]);
  public completedTriage = signal<ClinicalCareDetails[]>([]);

  public breadcrumbItems: MenuItem[] = [];

  ngOnInit(): void {
    this.loadAllData();
    this.breadcrumbItems = [
      { label: 'Home', routerLink: '/' },
      { label: 'Triagem' }
    ];
  }

  protected loadAllData(): void {
    forkJoin({
      awaiting: this.careService.getAwaitingTriage(),
      completed: this.careService.getCompletedTriage()
    }).subscribe({
      next: ({ awaiting, completed }) => {
      this.awaitingTriage.set(awaiting ?? []);
      this.completedTriage.set(completed ?? []);
      },
      error: () => {
      this.messageService.add({
        severity: 'error',
        summary: 'Erro',
        detail: 'Não foi possível carregar os atendimentos.'
      });
      }
    });
  }

  protected openTriageForm(careId: number): void {
    const ref = this.dialogService.open(TriageFormComponent, {
      header: 'Preencher Triagem',
      width: '40%',
      data: { clinicalCareId: careId }
    });

    if (ref) {
      ref.onClose.subscribe((success: boolean) => {
        if (success) {
          this.loadAllData();
          this.messageService.add({ severity: 'success', summary: 'Sucesso', detail: 'Triagem salva.' });
        }
      });
    }
  }

  protected openEditForm(triageId: number): void {
    console.log('Editar Triagem ID:', triageId);
  }

  protected deleteTriage(triageId: number): void {
    this.confirmationService.confirm({
      message: 'Tem certeza que deseja excluir esta triagem?',
      accept: () => {
        this.triageService.delete(triageId).subscribe(() => {
          this.loadAllData();
          this.messageService.add({ severity: 'success', summary: 'Excluído', detail: 'Triagem excluída.' });
        });
      }
    });
  }
}
