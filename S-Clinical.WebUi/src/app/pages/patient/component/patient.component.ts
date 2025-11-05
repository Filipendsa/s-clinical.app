import { Component, inject, OnInit, signal } from '@angular/core';
import { Toolbar } from '../../../shared/components/toolbar/toolbar';
import { PatientFormComponent } from '../components/patient-form.component/patient-form.component';
import { PatientService } from '../../../core/services/patient.service';
import { DialogService, DynamicDialogModule } from 'primeng/dynamicdialog';
import { ConfirmationService, MenuItem, MessageService } from 'primeng/api';
import { PatientModel } from '../../../core/models/patient.model';
import { GenderType } from '../../../core/enum/gender-type.enum';
import { CommonModule } from '@angular/common';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { ToastModule } from 'primeng/toast';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { CardModule } from 'primeng/card';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { TagModule } from 'primeng/tag';

@Component({
  selector: 'app-patient.component',
  imports: [
    Toolbar,
    CommonModule,

    TableModule,
    ButtonModule,
    DynamicDialogModule,
    ToastModule,
    ConfirmDialogModule,
    CardModule,
    BreadcrumbModule,
    TagModule
  ],
  templateUrl: './patient.component.html',
  styleUrl: './patient.component.scss',
})
export class PatientComponent implements OnInit {
  private patientService = inject(PatientService);
  private dialogService = inject(DialogService);
  private messageService = inject(MessageService);
  private confirmationService = inject(ConfirmationService);


  public patients = signal<PatientModel[]>([]);

  public breadcrumbItems: MenuItem[] = [];

  ngOnInit(): void {
    this.loadPatients();


    this.breadcrumbItems = [
      { label: 'Home', routerLink: '/' },
      { label: 'Paciente' }
    ];
  }

  loadPatients(): void {
    this.patientService.getAll().subscribe({
      next: (patients) => {
      if (!patients) return;
      this.patients.set(patients);
      },
      error: (err) => {
      this.messageService.add({
        severity: 'error',
        summary: 'Erro',
        detail: 'Não foi possível carregar os pacientes.'
      });
      }
    });
  }


  openFormModal(patientId?: number): void {
    const ref = this.dialogService.open(PatientFormComponent, {
      header: patientId ? 'Editar Paciente' : 'Novo Paciente',
      width: '40%', 
      styleClass: 'patient-form-dialog', 
      data: { id: patientId } 
    });

    if (ref) {
      ref.onClose.subscribe((success: boolean) => {
        if (success) {
          this.loadPatients();
          this.messageService.add({
            severity: 'success',
            summary: 'Sucesso',
            detail: 'Paciente salvo.'
          });
        }
      });
    }
  }

  deletePatient(id: number): void {
    this.confirmationService.confirm({
      message: 'Tem certeza que deseja excluir este paciente?',
      header: 'Confirmação',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.patientService.delete(id).subscribe({
          next: () => {
            this.loadPatients();
            this.messageService.add({
              severity: 'success',
              summary: 'Excluído',
              detail: 'Paciente excluído com sucesso.'
            });
          },
          error: (err) => {
            this.messageService.add({
              severity: 'error',
              summary: 'Erro',
              detail: 'Não foi possível excluir o paciente.'
            });
          }
        });
      }
    });
  }

  formatGender(gender: GenderType): string {
    switch (gender) {
      case GenderType.MALE: return 'Masculino';
      case GenderType.FEMALE: return 'Feminino';
      case GenderType.OTHER: return 'Outro';
      default: return 'Não informado';
    }
  }
}