# S-Clinical: Documentação de Sistema

## 1. Documentação de Requisitos

### 1.1. Objetivo
Descrever as funcionalidades principais e os requisitos que a aplicação S-Clinical deve atender, focando no gerenciamento de fluxo de pacientes em um ambiente clínico ou hospitalar, desde o registro inicial até a triagem.

### 1.2. Requisitos Funcionais (RF)

* **RF-i: Gestão de Pacientes (CRUD)**
    * O sistema deve permitir o cadastro, leitura, atualização e exclusão de pacientes.
    * Campos obrigatórios: nome, telefone, sexo (gênero) e e-mail.
* **RF-ii: Geração de Atendimento (Fila)**
    * O sistema deve permitir a abertura de um novo atendimento para um paciente (existente ou novo).
    * Ao criar um atendimento, o sistema deve gerar um número sequencial único para o dia (ex: 1, 2, 3...), que define a ordem da fila.
* **RF-iii: Registro de Triagem (Classificação)**
    * O sistema deve permitir que um profissional de enfermagem preencha a triagem de um paciente.
    * Campos obrigatórios: sintomas, pressão arterial, peso, altura, direcionamento para especialidade e classificação de risco manual (Prioridade: Azul, Verde, Amarelo, Laranja, Vermelho).
* **RF-iv: Gerenciamento de Fila de Triagem**
    * A tela de triagem deve exibir duas listas (abas):
        1.  **Aguardando:** Pacientes com status "Aguardando Triagem" (WAITING_TRIAGE), ordenados pelo número sequencial (RF-ii).
        2.  **Concluídos:** Pacientes que já passaram pela triagem.
* **RF-v: Transição de Status**
    * Ao criar um Atendimento (RF-ii), o status deve ser `WAITING_TRIAGE`.
    * Ao preencher a Triagem (RF-iii), o status do Atendimento deve ser atualizado para `WAITING_CARE` (Aguardando Atendimento Médico).
    * O sistema deve permitir outras transições (ex: `IN_MEDICATION`, `DISCHARGED`).

### 1.3. Requisitos Não Funcionais (RNF)

* **RNF-i: Interface de Usuário (UI)**
    * A interface deve ser amigável e responsiva.
    * Tecnologia: Angular (v17+ Standalone) com a biblioteca de componentes PrimeNG e o tema Aura (light/sky/gray).
* **RNF-ii: Desempenho**
    * O tempo de resposta da API para requisições de leitura (listas, detalhes) deve ser de até 2 segundos.
* **RNF-iii: Segurança de Dados**
    * Os dados de pacientes devem ser armazenados de forma segura no banco de dados.
    * A API deve implementar CORS para permitir a comunicação segura apenas com o frontend (`http://localhost:4200`).
* **RNF-iv: Arquitetura Backend**
    * O backend deve seguir os princípios da Clean Architecture, separando as camadas de Domínio, Aplicação e Infraestrutura.
* **RNF-v: Arquitetura de Comunicação**
    * A lógica de negócio deve ser desacoplada dos Controllers de API usando o padrão CQRS (Command Query Responsibility Segregation) com a biblioteca MediatR.
* **RNF-vi: Persistência de Dados**
    * O sistema deve usar o Entity Framework Core (EF Core) para o mapeamento objeto-relacional (ORM).
    * O banco de dados de desenvolvimento deve ser o SQL Server, executado em um contêiner Docker.

---

## 2. Arquitetura do Sistema

### 2.1. Backend (.NET 8)

O backend segue a **Clean Architecture** e o padrão **CQRS**.

* **`S-Clinical.Domain`**: Camada mais interna. Contém as Entidades (DDD, com `private set` e métodos de comportamento como `UpdateStatus`), os `Enums` e as Interfaces dos Repositórios (`IPatientRepository`, `IUnitOfWork`).
* **`S-Clinical.Application`**: Camada de lógica de negócio.
    * Contém os **Commands** (ações de escrita, ex: `CreatePatientCommand`) e **Queries** (ações de leitura, ex: `GetAwaitingTriageQuery`).
    * Contém os **Handlers** (ex: `CreateTriageCommandHandler`) que orquestram a lógica, injetando os repositórios e a `IUnitOfWork`.
* **`S-Clinical.Infrastructure`**: Camada externa.
    * Contém o `AppDbContext` do EF Core.
    * Contém as implementações dos Repositórios (ex: `PatientRepository`), que traduzem as chamadas do Handler em consultas LINQ.
* **`S-Clinical (API)`**: Ponto de entrada (Host).
    * Contém os `Controllers` (ex: `PatientController`).
    * Controllers são "magros" (skinny) e injetam apenas o `IMediator`. Eles não têm lógica de negócio.
    * `Program.cs` é responsável pelo registro de todos os serviços (DI), configuração do CORS e do `DbContext`.

### 2.2. Frontend (Angular)

O frontend utiliza a arquitetura **Standalone** baseada em `ApplicationConfig`.

* **`core/`**: Contém a lógica central "invisível" ao usuário.
    * `services/`: (ex: `PatientService`, `ClinicalCareService`, `TriageService`) - Responsáveis por todas as chamadas `HttpClient` para a API.
    * `models/`: Contém as interfaces TypeScript (DTOs) e `enums` que espelham o backend.
* **`pages/`**: Contém os componentes "inteligentes" (telas principais).
    * `atendimento/`: Tela de gestão de atendimentos (`atendimento.component.ts`).
    * `paciente/`: Tela de gestão de pacientes (`patient-list.component.ts`).
    * `triagem/`: Tela de gestão da fila de triagem (`triage-page.component.ts`).
    * `form-clinical-care/`, `form-patient/`, `form-triage/`: Componentes de formulário, abertos em modais.
* **`shared/`**: Contém componentes reutilizáveis ("burros").
    * `toolbar/`: (Ex: O menu de navegação principal `<app-toolbar>`).
* **`app.config.ts`**: Ponto central de registro. Configura o `provideHttpClient()`, `provideAnimations()`, e os serviços do PrimeNG (`DialogService`, `MessageService`).

---

## 3. Documentação do Backend (API)

### 3.1. Modelo de Dados (Entidades)

| Tabela | Chave Primária | Relacionamentos Chave | Propósito |
| :--- | :--- | :--- | :--- |
| `Patient` | `Id` (int) | Possui 1:M com `ClinicalCare` | Armazena dados demográficos dos pacientes. |
| `ClinicalCare` | `Id` (int) | Pertence 1:1 ao `Patient`. Possui 1:1 com `Triage`. | Representa um evento de atendimento (a "visita"). Controla a fila e o status. |
| `Triage` | `Id` (int) | Pertence 1:1 ao `ClinicalCare`. | Armazena os dados vitais e a classificação de risco. |
| (Enums) | `Id` (int) | `SpecialtyTypeEnum`, `PriorityLevelEnum`, `StatusCareEnum` | Tabelas de apoio para os dropdowns e status. |

### 3.2. Endpoints da API (Resumo)

#### `PatientController`
* `POST /api/Patient`: (CreatePatientCommand) - Cria um novo paciente.
* `GET /api/Patient`: (GetAllPatientsQuery) - Retorna uma lista de todos os pacientes.
* `GET /api/Patient/{id}`: (GetPatientByIdQuery) - Retorna um paciente específico.
* `PUT /api/Patient/{id}`: (UpdatePatientCommand) - Atualiza um paciente existente.
* `DELETE /api/Patient/{id}`: (DeletePatientCommand) - Exclui um paciente.

#### `ClinicalCareController`
* `POST /api/ClinicalCare`: (CreateClinicalCareCommand) - Abre um novo atendimento (Processo 2.0). Define o status como `WAITING_TRIAGE`.
* `GET /api/ClinicalCare/next-sequential`: (GetNextSequentialQuery) - Retorna o próximo número sequencial da fila do dia (ex: `{ "nextNumber": 101 }`).
* `GET /api/ClinicalCare/awaiting-triage`: (GetAwaitingTriageQuery) - Retorna a lista de atendimentos com status `WAITING_TRIAGE`, ordenados por `SequentialNumber`.
* `GET /api/ClinicalCare/completed-triage`: (GetCompletedTriageQuery) - Retorna a lista de atendimentos que já passaram da triagem.
* `GET /api/ClinicalCare/{id}`: (GetClinicalCareByIdQuery) - Retorna um atendimento detalhado (com Paciente e Triagem).
* `DELETE /api/ClinicalCare/{id}`: (DeleteClinicalCareCommand) - Exclui um atendimento.
* `PATCH /api/ClinicalCare/{id}/status`: (UpdateClinicalCareStatusCommand) - Atualiza o status de um atendimento (ex: para `IN_MEDICATION`).

#### `TriageController`
* `POST /api/Triage`: (CreateTriageCommand) - (Processo 3.0) Cria um novo registro de triagem. O Handler também atualiza o status do `ClinicalCare` (de `WAITING_TRIAGE` para `WAITING_CARE`).
* `GET /api/Triage/{id}`: (GetTriageByIdQuery) - Retorna um registro de triagem específico.
* `PUT /api/Triage/{id}`: (UpdateTriageCommand) - Atualiza um registro de triagem.
* `DELETE /api/Triage/{id}`: (DeleteTriageCommand) - Exclui um registro de triagem.

---

## 4. Documentação do Frontend (Angular)

### 4.1. Fluxo de Componentes (Páginas)

* **`AtendimentoComponent` (Página de Atendimento):**
    * No `ngOnInit`, chama o `clinicalCareService.getAwaitingTriage()` (ou `GetAll`) e preenche o `p-table`.
    * O botão "Novo Atendimento" chama o `dialogService.open(FormClinicalCareComponent)`.
    * `FormClinicalCareComponent` (Modal):
        * No `ngOnInit`, chama `patientService.getAll()` (para o `<p-dropdown>`) e `clinicalCareService.getNextSequential()` (para o número da senha).
        * Permite redirecionar para `/paciente` se o paciente não existir.
        * Ao salvar, chama `clinicalCareService.create()`.

* **`PatientListComponent` (Página de Paciente):**
    * No `ngOnInit`, chama o `patientService.getAll()` e preenche o `p-table`.
    * O botão "Novo Paciente" chama `dialogService.open(FormPatientComponent)`.
    * `FormPatientComponent` (Modal):
        * Implementa um `FormGroup` com `Validators` (required, email, minLength).
        * No `ngOnInit`, verifica se um `data.id` foi passado para carregar os dados em modo de edição.
        * Ao salvar, chama `patientService.create()` ou `patientService.update()`.

* **`TriagePageComponent` (Página de Triagem - Processo 3.0):**
    * Componente principal de fluxo de trabalho.
    * Usa `<p-tabView>` para exibir duas abas:
        1.  **Aba "Aguardando":** No `ngOnInit`, chama `clinicalCareService.getAwaitingTriage()` e preenche a `p-table` (ordenada por senha).
        2.  **Aba "Concluídos":** Chama `clinicalCareService.getCompletedTriage()`.
    * Na Aba 1, o botão "Preencher Triagem" chama `dialogService.open(FormTriageComponent)`, passando o `clinicalCareId`.
    * `FormTriageComponent` (Modal):
        * Recebe o `clinicalCareId`.
        * Exibe um formulário completo (sintomas, pressão, peso, altura, especialidade, prioridade).
        * Ao salvar, chama `triageService.create()`. O backend (API) cuida de atualizar o status do `ClinicalCare` automaticamente.

---

## 5. Setup de Ambiente (Guia Rápido)

### 5.1. Banco de Dados (SQL Server no Docker)
1.  Verifique se o Docker Desktop está em execução.
2.  Rode o comando (no terminal) para iniciar um contêiner SQL Server com dados persistentes:
    ```bash
    docker volume create sql-data-volume
    docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=SuaSenhaForte!123" -p 1433:1433 --name sql-server-dev -v sql-data-volume:/var/opt/mssql -d [mcr.microsoft.com/mssql/server:2022-latest](https://mcr.microsoft.com/mssql/server:2022-latest)
    ```

### 5.2. Backend (.NET API)
1.  Abra o projeto `S-Clinical.sln` no Visual Studio.
2.  Verifique se a `ConnectionString` no `appsettings.json` está correta:
    ```json
    "DefaultConnection": "Server=localhost;Database=s_clinical;User ID=sa;Password=SuaSenhaForte!123;TrustServerCertificate=True"
    ```
3.  Abra o "Package Manager Console" e rode `Update-Database` para criar o schema.
4.  Execute (Run) o projeto (deve iniciar em `https://localhost:7154`).

### 5.3. Frontend (Angular UI)
1.  Abra a pasta do projeto Angular (ex: `S-Clinical.WebUi`) no VS Code.
2.  Rode `npm install` para baixar as dependências (Angular, PrimeNG).
3.  (Se for a primeira vez) Instale o Angular CLI globalmente: `npm install -g @angular/cli`.
4.  Rode `ng serve` para iniciar o servidor de desenvolvimento.
5.  Acesse `http://localhost:4200` no navegador.
6.  (Primeira vez) Aceite o certificado SSL do backend acessando `https://localhost:7154/api/Patient` e clicando em "Avançado" > "Ir para localhost (não seguro)".