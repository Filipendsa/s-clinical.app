-----

# S-Clinical WebUI (Frontend)

Este projeto é a interface de usuário (Web UI) do sistema de gerenciamento hospitalar S-Clinical. Ele é construído com **Angular (v17+ Standalone)** e utiliza a biblioteca de componentes **PrimeNG** com o tema **Aura**.

Esta aplicação é o "cliente" e depende 100% do [S-Clinical .NET API](https://www.google.com/search?q=https://github.com/seu-link-para-o-backend) (o "servidor") para funcionar.

## 🚀 Guia de Setup e Execução

Para rodar este projeto, o Backend (.NET API) **deve** estar em execução.

### Pré-requisitos

  * [Node.js](https://nodejs.org/) (versão LTS)
  * Angular CLI: `npm install -g @angular/cli`
  * A [API .NET do S-Clinical](https://www.google.com/search?q=https://github.com/seu-link-para-o-backend) deve estar rodando (geralmente em `https://localhost:7154`).

### 1\. Instale as Dependências

Na pasta raiz do projeto, rode:

```bash
npm install
```

### 2\. Execute o Servidor de Desenvolvimento

Rode o comando para iniciar o servidor do Angular (padrão `http://localhost:4200/`):

```bash
ng serve
```

### 🚨 IMPORTANTE: Erro de SSL (Primeira Execução)

Na primeira vez que você rodar o projeto, o navegador **vai falhar** em se conectar com a API, mostrando erros no console como `net::ERR_FAILED` ou `HttpErrorResponse { status: 0 }`.

Isso acontece porque a API .NET usa um certificado SSL de desenvolvimento (`https://`) que o seu navegador não confia por padrão.

**Para corrigir (faça isso apenas uma vez):**

1.  Com a API .NET rodando, abra uma nova aba no seu navegador.
2.  Navegue diretamente para a URL da sua API, por exemplo: `https://localhost:7154/api/Patient`
3.  O navegador mostrará um aviso de segurança ("Sua conexão não é particular").
4.  Clique em **"Avançado"**.
5.  Clique em **"Ir para localhost (não seguro)"**.
6.  Você verá o JSON da sua API. Agora o seu navegador confia no certificado.
7.  **Volte para a aba do Angular (`http://localhost:4200/`) e atualize a página.** O aplicativo funcionará.

## 🛠️ Principais Tecnologias

  * **Framework:** Angular (v17+ Standalone)
  * **Biblioteca de UI:** [PrimeNG](https://primeng.org/)
  * **Tema:** [PrimeNG Aura (@primeuix/themes)](https://www.google.com/search?q=https://primeng.org/aura)
      * **Modo:** `aura-light` (Claro)
      * **Cor Primária:** `sky` (Azul claro)
      * **Superfície:** `gray` (Cinza claro/Branco)
  * **Gerenciamento de Estado:** Baseado em Serviços (`PatientService`, etc.) e Signals
  * **Modais:** `DialogService` do PrimeNG

## 📁 Arquitetura do Frontend

O projeto segue uma arquitetura baseada em funcionalidades (Feature-based).

  * `src/app/core/`: Contém a lógica central.
      * `services/`: Serviços HTTP (`PatientService`, `ClinicalCareService`, `TriageService`).
      * `models/`: Interfaces TypeScript e Enums (DTOs) que espelham a API.
  * `src/app/pages/`: Componentes "inteligentes" (as páginas principais).
      * `atendimento/`: Lista de atendimentos (Processo 2.0).
      * `paciente/`: CRUD de pacientes.
      * `triagem/`: Fila de triagem (Processo 3.0).
      * `form-.../`: Componentes de formulário abertos em modais pelo `DialogService`.
  * `src/app/shared/`: Componentes reutilizáveis ("burros"), como a `ToolbarComponent`.
  * `src/app/app.config.ts`: Arquivo central de configuração (registra `provideHttpClient`, `provideAnimations`, `DialogService`, etc.).
  * `src/styles.scss`: Onde o tema `@primeuix/themes/aura` é importado globalmente.

## ✨ Comandos Úteis do Angular CLI

### Gerar Componentes

Rode `ng generate component component-name` (ou `ng g c component-name`) para gerar um novo componente.

*Para gerar um novo componente de página (standalone):*

```bash
ng g c pages/minha-nova-pagina --standalone
```

### Build de Produção

Rode `ng build` para compilar o projeto. Os arquivos otimizados são salvos na pasta `dist/`.