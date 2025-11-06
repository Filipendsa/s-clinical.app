import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable, of } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ClinicalCareDetails } from '../models/clinical-care.model';
import { QueueSummary } from '../../shared/components/toolbar/toolbar';


/**
 * Serviço responsável por gerenciar as operações de CRUD (Create, Read, Update, Delete)
 * e consultas especializadas para a entidade 'ClinicalCare' (Atendimento Clínico).
 *
 * Este serviço lida com a comunicação com a API para buscar, criar, modificar,
 * deletar atendimentos, bem como obter listas filtradas (ex: aguardando triagem)
 * e o próximo número sequencial.
 */
@Injectable({
  providedIn: 'root'
})
export class ClinicalCareService {
  /** Injeção do HttpClient para realizar requisições HTTP. */
  private http = inject(HttpClient);

  /** URL base da API para os endpoints de ClinicalCare, vinda do environment. */
  private apiUrl = `${environment.apiUrl}/ClinicalCare`;

  /**
   * Busca todos os registros de Atendimento Clínico.
   * Utiliza o método GET.
   *
   * @returns Um Observable que emite um array de ClinicalCareDetails[].
   */
  public getAll(): Observable<ClinicalCareDetails[]> {
    return this.http.get<ClinicalCareDetails[]>(this.apiUrl);
  }

  /**
   * Busca um Atendimento Clínico específico pelo seu ID.
   * Utiliza o método GET.
   *
   * @param id O ID numérico do atendimento a ser buscado.
   * @returns Um Observable que emite o ClinicalCareDetails correspondente.
   */
  public getById(id: number): Observable<ClinicalCareDetails> {
    return this.http.get<ClinicalCareDetails>(`${this.apiUrl}/${id}`);
  }

  /**
   * Envia uma requisição para criar um novo registro de Atendimento Clínico.
   * Utiliza o método POST.
   *
   * @param command O modelo com os dados para a criação (tipo 'any').
   * @returns Um Observable que emite o ClinicalCareDetails recém-criado.
   */
  public create(command: any): Observable<ClinicalCareDetails> {
    return this.http.post<ClinicalCareDetails>(this.apiUrl, command);
  }

  /**
   * Envia uma requisição para atualizar um Atendimento Clínico existente.
   * Utiliza o método PUT.
   *
   * @param id O ID numérico do atendimento a ser atualizado.
   * @param command O modelo com os dados a serem atualizados (tipo 'any').
   * @returns Um Observable<void> que completa quando a atualização é bem-sucedida.
   */
  public update(id: number, command: any): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, command);
  }

  /**
   * Envia uma requisição para deletar um Atendimento Clínico.
   * Utiliza o método DELETE.
   *
   * @param id O ID numérico do atendimento a ser deletado.
   * @returns Um Observable<void> que completa quando a deleção é bem-sucedida.
   */
  public delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  /**
   * Busca na API o próximo número sequencial disponível para um novo atendimento.
   * Consome o endpoint '.../next-sequential' e extrai a propriedade 'nextNumber'.
   *
   * @returns Um Observable que emite o próximo número (number).
   */
  public getNextSequential(): Observable<number> {
    return this.http.get<{ nextNumber: number }>(`${this.apiUrl}/next-sequential`)
      .pipe(
        map(response => response.nextNumber)
      );
  }

  /**
   * Busca uma lista de Atendimentos Clínicos que estão com status 'Aguardando Triagem'.
   * Consome o endpoint '.../awaiting-triage'.
   *
   * @returns Um Observable que emite um array de ClinicalCareDetails[].
   */
  public getAwaitingTriage(): Observable<ClinicalCareDetails[]> {
    return this.http.get<ClinicalCareDetails[]>(`${this.apiUrl}/awaiting-triage`);
  }

  /**
   * Busca uma lista de Atendimentos Clínicos que já tiveram a triagem concluída.
   * Consome o endpoint '.../completed-triage'.
   *
   * @returns Um Observable que emite um array de ClinicalCareDetails[].
   */
  public getCompletedTriage(): Observable<ClinicalCareDetails[]> {
    return this.http.get<ClinicalCareDetails[]>(`${this.apiUrl}/completed-triage`);
  }

  /**
   * Retrieves a summary of the current clinical care queue.
   *
   * @returns An Observable emitting the {@link QueueSummary} object containing queue details.
   */
  public getQueueSummary(): Observable<QueueSummary> {
    return this.http.get<QueueSummary>(`${this.apiUrl}/queue-summary`);
  }

}