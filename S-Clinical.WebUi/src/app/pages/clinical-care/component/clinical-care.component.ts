import { Component, OnInit, Signal, WritableSignal, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { DialogService, DynamicDialogModule } from 'primeng/dynamicdialog';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { CardModule } from 'primeng/card';
import { ConfirmationService, MenuItem, MessageService } from 'primeng/api';

import { Observable } from 'rxjs';
import { Toolbar } from '../../../shared/components/toolbar/toolbar';
import { DialogModule } from 'primeng/dialog';
import { ClinicalCareService } from '../../../core/services/clinical-care.service';
import { ClinicalCareDetails } from '../../../core/models/clinical-care.model';
import { ClinicalCareFormComponent } from '../components/clinical-care-form.component/clinical-care-form.component';
import { TagModule } from 'primeng/tag';
import { BreadcrumbModule } from 'primeng/breadcrumb';

@Component({
  selector: 'app-clinical-care.component',
  imports: [Toolbar, DialogModule,
    CommonModule,
    TableModule,
    ButtonModule,
    DynamicDialogModule,
    ToastModule,
    ConfirmDialogModule,
    CardModule,
    TagModule,
    BreadcrumbModule,
    TagModule
  ],
  templateUrl: './clinical-care.component.html',
  styleUrl: './clinical-care.component.scss',
})
export class ClinicalCareComponent implements OnInit {

  private careService = inject(ClinicalCareService);
  private dialogService = inject(DialogService);
  private messageService = inject(MessageService);
  private confirmationService = inject(ConfirmationService);

  public clinicalCares: WritableSignal<ClinicalCareDetails[]> = signal<ClinicalCareDetails[]>([]);
  public breadcrumbItems: MenuItem[] = [];

  ngOnInit(): void {
    this.loadCares();
    this.breadcrumbItems = [
      { label: 'Home', routerLink: '/' },
      { label: 'Atendimento' }
    ];
  }

  loadCares(): void {
    this.careService.getAll().subscribe({
      next: (cares) => {
        if (!cares) return;
        this.clinicalCares.set(cares);
      },
      error: (err) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Erro',
          detail: 'Não foi possível carregar os atendimentos.'
        });
      }
    });
  }

  openFormModal(careId?: number): void {
    const ref = this.dialogService.open(ClinicalCareFormComponent, {
      header: careId ? 'Editar Atendimento' : 'Novo Atendimento',
      width: '40%',
      styleClass: 'care-form-dialog',
      data: {
        id: careId
      }
    });

    if (ref) {
      ref.onClose.subscribe((success: boolean) => {
        if (success) {
          this.loadCares();
          this.messageService.add({
            severity: 'success',
            summary: 'Sucesso',
            detail: 'Atendimento salvo.'
          });
        }
      });
    }
  }

  deleteCare(id: number): void {
    this.confirmationService.confirm({
      message: 'Tem certeza que deseja excluir este atendimento?',
      header: 'Confirmação',
      icon: 'pi pi-exclamation-triangle',
      acceptLabel: 'Sim, excluir',
      rejectLabel: 'Cancelar',
      accept: () => {
        this.careService.delete(id).subscribe({
          next: () => {
            this.loadCares();
            this.messageService.add({
              severity: 'success',
              summary: 'Excluído',
              detail: 'Atendimento excluído com sucesso.'
            });
          },
          error: (err) => {
            this.messageService.add({
              severity: 'error',
              summary: 'Erro',
              detail: 'Não foi possível excluir o atendimento.'
            });
          }
        });
      }
    });
  }

  getStatusSeverity(status: string): 'success' | 'secondary' | 'info' | 'warn' | 'danger' | 'contrast' | undefined | null {
    switch (status) {
      case 'WAITING_CARE':
        return 'warn';
      case 'RECEIVING_CARE':
        return 'info';
      case 'IN_TRIAGE':
        return 'info';
      case 'DISCHARGED':
        return 'success';
      case 'DECEASED':
        return 'danger';
      default:
        return 'secondary';
    }
  }
}
