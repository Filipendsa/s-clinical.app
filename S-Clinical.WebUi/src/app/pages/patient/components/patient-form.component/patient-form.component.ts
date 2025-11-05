import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { InputTextModule } from 'primeng/inputtext';
import { MessageModule } from 'primeng/message';
import { SelectModule } from 'primeng/select';
import { SkeletonModule } from 'primeng/skeleton';
import { PatientService } from '../../../../core/services/patient.service';
import { GenderType } from '../../../../core/enum/gender-type.enum';
import { CreatePatientModel, UpdatePatientModel } from '../../../../core/models/patient.model';

@Component({
  selector: 'app-patient-form.component',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ButtonModule,
    InputTextModule,
    SelectModule,
    SkeletonModule,
    MessageModule
  ],
  templateUrl: './patient-form.component.html',
  styleUrl: './patient-form.component.scss',
})
export class PatientFormComponent implements OnInit {


  private ref = inject(DynamicDialogRef);
  private config = inject(DynamicDialogConfig);
  private fb = inject(FormBuilder);
  private patientService = inject(PatientService);

  public patientForm!: FormGroup;
  private editingId: number | null = null;
  public isLoading = signal(false);
  public genderOptions: any[] = []; 

  constructor() {

    this.genderOptions = [
      { label: 'Masculino', value: GenderType.MALE },
      { label: 'Feminino', value: GenderType.FEMALE },
      { label: 'Outro', value: GenderType.OTHER }
    ];
  }

  ngOnInit(): void {
    this.editingId = this.config.data?.id || null;

    this.patientForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      phoneNumber: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      gender: [null, [Validators.required]]
    });

    if (this.editingId) {
      this.isLoading.set(true);
      this.patientService.getById(this.editingId).subscribe(data => {
        this.patientForm.patchValue(data);
        this.isLoading.set(false);
      });
    }
  }

  save(): void {
    if (this.patientForm.invalid) {
      this.patientForm.markAllAsTouched(); 
      return;
    }
    
    this.isLoading.set(true);
    const formValue = this.patientForm.value;

    if (this.editingId) {
      const updateDto: UpdatePatientModel = { id: this.editingId, ...formValue };
      
      this.patientService.update(this.editingId, updateDto).subscribe({
        next: () => this.closeModal(true),
        error: (err) => this.handleError(err),
        complete: () => this.isLoading.set(false)
      });

    } else {
      const createDto: CreatePatientModel = formValue;
      
      this.patientService.create(createDto).subscribe({
        next: () => this.closeModal(true),
        error: (err) => this.handleError(err),
        complete: () => this.isLoading.set(false)
      });
    }
  }

  handleError(error: any): void {
    console.error('Erro ao salvar:', error);
    this.isLoading.set(false);
  }

  closeModal(success: boolean): void {
    this.ref.close(success);
  }

  get f() { return this.patientForm.controls; }
}