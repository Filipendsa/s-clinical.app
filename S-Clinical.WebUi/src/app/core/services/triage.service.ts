import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { CreateTriageModel, TriageModel, UpdateTriageModel } from '../models/triage.model';

/**
 * Serviço responsável por gerenciar as operações de CRUD (Create, Update, Delete)
 * para a entidade 'Triage' (Triagem).
 *
 * Este serviço se comunica com a API backend para persistir e modificar
 * os dados de triagem.
 */
@Injectable({
  providedIn: 'root'
})
export class TriageService {
  /** Injeção do HttpClient para realizar requisições HTTP. */
  private http = inject(HttpClient);

  /** URL base da API para os endpoints de Triage, vinda do environment. */
  private apiUrl = `${environment.apiUrl}/Triage`;

  /**
   * Envia uma requisição para criar um novo registro de Triagem.
   * Utiliza o método POST.
   *
   * @param command O modelo (CreateTriageModel) com os dados para a criação.
   * @returns Um Observable que emite o TriageModel recém-criado pela API.
   */
  public create(command: CreateTriageModel): Observable<TriageModel> {
    return this.http.post<TriageModel>(this.apiUrl, command);
  }

  /**
   * Envia uma requisição para atualizar um registro de Triagem existente.
   * Utiliza o método PUT.
   *
   * @param id O ID numérico da Triagem a ser atualizada.
   * @param command O modelo (UpdateTriageModel) com os dados a serem atualizados.
   * @returns Um Observable<void> que completa quando a atualização é bem-sucedida.
   */
  public update(id: number, command: UpdateTriageModel): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, command);
  }

  /**
   * Envia uma requisição para deletar um registro de Triagem.
   * Utiliza o método DELETE.
   *
   * @param id O ID numérico da Triagem a ser deletada.
   * @returns Um Observable<void> que completa quando a deleção é bem-sucedida.
   */
  public delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}