import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { DynamicDialogRef, DynamicDialogConfig } from 'primeng/dynamicdialog';
import { SkeletonModule } from 'primeng/skeleton';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ClinicalCareService } from '../../../../core/services/clinical-care.service';
import { PatientService } from '../../../../core/services/patient.service';
import { PatientModel } from '../../../../core/models/patient.model';
import { SelectModule } from 'primeng/select';
import { MessageModule } from 'primeng/message';
import { forkJoin } from 'rxjs';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-clinical-care-form.component',
  imports: [CommonModule, ButtonModule, InputTextModule, ReactiveFormsModule, SelectModule, SkeletonModule, MessageModule],
  templateUrl: './clinical-care-form.component.html',
  styleUrl: './clinical-care-form.component.scss',
})
export class ClinicalCareFormComponent implements OnInit {
  private ref = inject(DynamicDialogRef);
  private config = inject(DynamicDialogConfig);
  private fb = inject(FormBuilder);
  private router = inject(Router);
  private careService = inject(ClinicalCareService);
  private patientService = inject(PatientService);
  private messageService = inject(MessageService);

  public careForm!: FormGroup;
  public patients = signal<PatientModel[]>([]);
  protected editingId: number | null = null;
  public loading = signal(true);

  ngOnInit(): void {
    this.editingId = this.config.data?.id || null;
    this.careForm = this.fb.group({
      patientId: [null, [Validators.required]],
      sequentialNumber: [{ value: null, disabled: true }, [Validators.required]]
    });
    this.loadInitialData();
  }

  loadInitialData(): void {
    this.loading.set(true);
    const patients$ = this.patientService.getAll();
    const data$ = this.editingId
      ? this.careService.getById(this.editingId)
      : this.careService.getNextSequential();
    forkJoin({
      patients: patients$,
      data: data$
    }).subscribe(({ patients, data }) => {
      this.patients.set(patients);
      if (this.editingId && typeof data === 'object') {
        this.careForm.patchValue(data);
      } else if (!this.editingId && typeof data === 'number') {
        this.careForm.patchValue({ sequentialNumber: data });
      }
      this.loading.set(false);
    });
  }

  loadPatients(): void {}

  redirectToPatientForm(): void {
    this.ref.close(false);
    this.router.navigate(['/paciente']);
  }

  save(): void {
    if (this.careForm.invalid) {
      this.careForm.markAllAsTouched();
      return;
    }
    const formValue = this.careForm.getRawValue();
    this.careService.create(formValue).subscribe({
      next: () => {
        this.messageService.add({severity:'success', summary:'Sucesso', detail:'Registro criado com sucesso!'});
        this.closeModal(true);
      },
      error: () => {
        this.messageService.add({severity:'error', summary:'Erro', detail:'Erro ao criar registro.'});
      }
    });
  }

  closeModal(success: boolean): void {
    this.ref.close(success);
  }

  get f() { return this.careForm.controls; }
}
