import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CreatePatientModel, PatientModel, UpdatePatientModel } from '../models/patient.model';

/**
 * Serviço responsável por gerenciar as operações de CRUD (Create, Read, Update, Delete)
 * para a entidade 'Patient' (Paciente).
 *
 * Este serviço lida com toda a comunicação HTTP com a API backend
 * para buscar, criar, modificar e deletar dados de pacientes.
 */
@Injectable({
  providedIn: 'root'
})
export class PatientService {

  /** Injeção do HttpClient para realizar requisições HTTP. */
  private http = inject(HttpClient);

  /** URL base da API para os endpoints de Patient, vinda do environment. */
  private apiUrl = `${environment.apiUrl}/Patient`;

  constructor() { }

  /**
   * Busca todos os registros de Pacientes na API.
   * Utiliza o método GET.
   *
   * @returns Um Observable que emite um array de PatientModel[].
   */
  public getAll(): Observable<PatientModel[]> {
    return this.http.get<PatientModel[]>(this.apiUrl);
  }

  /**
   * Busca um Paciente específico pelo seu ID.
   * Utiliza o método GET.
   *
   * @param id O ID numérico do Paciente a ser buscado.
   * @returns Um Observable que emite o PatientModel correspondente.
   */
  public getById(id: number): Observable<PatientModel> {
    return this.http.get<PatientModel>(`${this.apiUrl}/${id}`);
  }

  /**
   * Envia uma requisição para criar um novo registro de Paciente.
   * Utiliza o método POST.
   *
   * @param patientData O modelo (CreatePatientModel) com os dados para a criação.
   * @returns Um Observable que emite o PatientModel recém-criado pela API.
   */
  public create(patientData: CreatePatientModel): Observable<PatientModel> {
    return this.http.post<PatientModel>(this.apiUrl, patientData);
  }

  /**
   * Envia uma requisição para atualizar um registro de Paciente existente.
   * Utiliza o método PUT.
   *
   * @param id O ID numérico do Paciente a ser atualizado.
   * @param patientData O modelo (UpdatePatientModel) com os dados a serem atualizados.
   * @returns Um Observable<void> que completa quando a atualização é bem-sucedida.
   */
  public update(id: number, patientData: UpdatePatientModel): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, patientData);
  }

  /**
   * Envia uma requisição para deletar um registro de Paciente.
   * Utiliza o método DELETE.
   *
   * @param id O ID numérico do Paciente a ser deletado.
   * @returns Um Observable<void> que completa quando a deleção é bem-sucedida.
   */
  public delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}