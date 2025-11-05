import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { InputNumberModule } from 'primeng/inputnumber';
import { MessageModule } from 'primeng/message';
import { SkeletonModule } from 'primeng/skeleton';
import { MessageService } from 'primeng/api';

import { DynamicDialogRef, DynamicDialogConfig } from 'primeng/dynamicdialog';
import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { SelectModule } from 'primeng/select';
import { SpecialtyTypeEnum } from '../../../../core/enum/speciality-type.enum';
import { TextareaModule } from 'primeng/textarea';
import { TriageService } from '../../../../core/services/triage.service';
import { CreateTriageModel } from '../../../../core/models/triage.model';
import { PriorityLevelEnum } from '../../../../core/enum/priority-level.enum';

@Component({
  selector: 'app-form-triage',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ButtonModule,
    InputTextModule,
    TextareaModule,
    SelectModule,
    InputNumberModule,
    MessageModule,
    SkeletonModule
  ],
  templateUrl: './triage-form.component.html',
  styleUrl: './triage-form.component.scss'
})
export class TriageFormComponent implements OnInit {

  private ref = inject(DynamicDialogRef);
  private config = inject(DynamicDialogConfig);
  private fb = inject(FormBuilder);
  private triageService = inject(TriageService);
  private messageService = inject(MessageService);

  public triageForm!: FormGroup;
  private clinicalCareId!: number;
  public isLoading = signal(false);

  public specialtyOptions: any[] = [];
  public priorityOptions: any[] = [];

  constructor() {
    this.specialtyOptions = [
      { label: 'Clínica Médica', value: SpecialtyTypeEnum.CLINICAL_MEDICINE },
      { label: 'Cardiologia', value: SpecialtyTypeEnum.CARDIOLOGY },
      { label: 'Neurologia', value: SpecialtyTypeEnum.NEUROLOGY },
      { label: 'Ortopedia', value: SpecialtyTypeEnum.ORTHOPEDICS },
      { label: 'Cirurgia Geral', value: SpecialtyTypeEnum.GENERAL_SURGERY },
      { label: 'Obstetrícia', value: SpecialtyTypeEnum.OBSTETRICS },
      { label: 'Pediatria', value: SpecialtyTypeEnum.PEDIATRICS }
    ];

    this.priorityOptions = [
      { label: 'Vermelho (Emergência)', value: PriorityLevelEnum.RED, severity: 'danger' },
      { label: 'Laranja (Muito Urgente)', value: PriorityLevelEnum.ORANGE, severity: 'warning' },
      { label: 'Amarelo (Urgente)', value: PriorityLevelEnum.YELLOW, severity: 'warning' },
      { label: 'Verde (Pouco Urgente)', value: PriorityLevelEnum.GREEN, severity: 'success' },
      { label: 'Azul (Não Urgente)', value: PriorityLevelEnum.BLUE, severity: 'info' }
    ];
  }

  ngOnInit(): void {
    this.clinicalCareId = this.config.data?.clinicalCareId;
    if (!this.clinicalCareId) {
      this.messageService.add({ severity: 'error', summary: 'Erro', detail: 'ID do Atendimento não encontrado.' });
      this.closeModal(false);
      return;
    }

    this.triageForm = this.fb.group({
      symptoms: ['', [Validators.required, Validators.minLength(10)]],
      bloodPressure: ['', [Validators.required]],
      weight: [null, [Validators.required, Validators.min(1)]],
      height: [null, [Validators.required, Validators.min(0.1)]],
      speciality: [null, [Validators.required]],
      priority: [null, [Validators.required]]
    });
  }

  save(): void {
    if (this.triageForm.invalid) {
      this.triageForm.markAllAsTouched();
      return;
    }

    this.isLoading.set(true);

    const formValue = this.triageForm.value;
    const createDto: CreateTriageModel = {
      clinicalCareId: this.clinicalCareId,
      symptoms: formValue.symptoms,
      bloodPressure: formValue.bloodPressure,
      weight: formValue.weight,
      height: formValue.height,
      speciality: formValue.speciality,
      priority: formValue.priority
    };

    this.triageService.create(createDto).subscribe({
      next: () => {
        this.isLoading.set(false);
        this.closeModal(true);
      },
      error: (err) => {
        this.isLoading.set(false);
        this.messageService.add({ severity: 'error', summary: 'Erro ao Salvar', detail: 'Não foi possível registrar a triagem.' });
        console.error('Erro ao salvar triagem:', err);
      }
    });
  }

  closeModal(success: boolean): void {
    this.ref.close(success);
  }

  get f() { return this.triageForm.controls; }
}
