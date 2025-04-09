# TaskManager API (.NET 9 + PostgreSQL + Minimal API)

Esta é uma API de gerenciamento de tarefas desenvolvida em **.NET 9** utilizando **Minimal API**, arquitetura limpa com separação em camadas e integração com **PostgreSQL** via **Docker**.

---

## 📦 Tecnologias utilizadas

- .NET 9 (Preview)
- Minimal API
- Entity Framework Core
- PostgreSQL (via Docker)
- CQRS (sem MediatR)
- Mapeamento com `implicit/explicit operator` (sem AutoMapper)
- FluentValidation
- Serilog para logs estruturados

---

## 📁 Estrutura do Projeto

| Projeto                       | Responsabilidade                                              |
|------------------------------|----------------------------------------------------------------|
| `TaskManager.Api`            | API HTTP com Minimal API e configuração de endpoints           |
| `TaskManager.Application`    | Handlers, comandos, queries, validadores e casos de uso        |
| `TaskManager.Core`           | Entidades de domínio e interfaces (contratos)                  |
| `TaskManager.Infrastructure` | Implementações de repositórios, EF Core, contexto, migrations  |

---

## 🚫 Por que não usei MediatR ou AutoMapper?

- **MediatR** e **AutoMapper** passaram a adotar modelos de licenciamento pago/comercial, o que **impacta diretamente a viabilidade de uso em projetos open-source ou profissionais** com orçamento restrito.
- Em substituição:
  - Usei **handlers diretos** com injeção de dependência (seguindo CQRS manual)
  - Usei **`implicit/explicit operator`** entre entidades e DTOs

---

## 🐳 Docker + PostgreSQL

A aplicação utiliza PostgreSQL com suporte a Docker Compose para fácil inicialização e persistência de dados.

### 📄 Arquivo `docker-compose.yml`

```yaml
version: '3.9'
services:
  db:
    image: postgres:16
    container_name: taskmanager_postgres
    restart: always
    environment:
      POSTGRES_DB: taskdb
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: postgres
    ports:
      - "5432:5432"
    volumes:
      - pgdata:/var/lib/postgresql/data

  api:
    build:
      context: .
      dockerfile: TaskManager.Api/Dockerfile
    container_name: taskmanager_api
    ports:
      - "5000:8080"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings__Default=Host=db;Port=5432;Database=taskdb;Username=postgres;Password=postgres
    depends_on:
      - db

volumes:
  pgdata:
```

### 🚀 Como subir o ambiente com Docker

1. Certifique-se de ter o Docker Desktop instalado e o WSL habilitado (se estiver no Windows)
2. Execute na raiz do projeto:

```bash
docker compose down -v
docker compose up -d --build
```

Acesse a aplicação:
📍 http://localhost:5000/swagger

---

## 🐚 Arquivo `wait-for-postgres.sh`

Esse arquivo foi adicionado ao projeto `TaskManager.Api` e é utilizado para garantir que o container da API **aguarde o banco de dados PostgreSQL estar disponível antes de iniciar**.

Isso evita erros de conexão quando o banco ainda está inicializando.

### Exemplo de uso no Dockerfile:
```dockerfile
COPY TaskManager.Api/wait-for-postgres.sh .
RUN chmod +x ./wait-for-postgres.sh
ENTRYPOINT ["./wait-for-postgres.sh", "db", "dotnet", "TaskManager.Api.dll"]
```

---

## ✅ Funcionalidades implementadas

- Cadastro de usuários
- Criação de projetos por usuário
- Adição, atualização e remoção de tarefas por projeto
- Comentários e histórico de alterações de tarefas
- Regras de negócio:
  - Validação de limite de tarefas por projeto
  - Proibição de exclusão de projeto com tarefas pendentes
  - Registro de todas as alterações de status/descrição
  - Acesso a relatórios (função de gerente)

---

## 🧪 Testes automatizados

O projeto possui cobertura ampla de testes unitários para:
- Handlers (Create, Update, Delete, Comment, Report)
- Validadores (com FluentValidation.TestHelper)

### Ferramentas utilizadas:
- xUnit
- FluentAssertions
- Moq
- FluentValidation.TestHelper

### Como executar testes com cobertura

1. Instale o coletor de cobertura:
```bash
dotnet add TaskManager.Tests package coverlet.collector
```

2. Execute os testes com coleta de cobertura:
```bash
dotnet test --collect:"XPlat Code Coverage"
```

3. Instale o report generator (caso ainda não tenha):
```bash
dotnet tool install --global dotnet-reportgenerator-globaltool
```

4. Gere o relatório HTML:
```bash
reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:coverage-report -reporttypes:Html
```

5. Abra no navegador:
```bash
start coverage-report/index.html  # Windows
open coverage-report/index.html   # macOS
xdg-open coverage-report/index.html # Linux
```

> A cobertura atual estimada está entre **75% e 85%**, com foco nos handlers e validações. Pode ser estendida com testes de integração.

---

## 📄 Documentação OpenAPI

A documentação da API está disponível automaticamente no Swagger:
📍 http://localhost:5000/swagger

Além disso, você pode importar o arquivo [openapi-spec.yaml](openapi-spec.yaml) ou [openapi-spec.json](openapi-spec.json) para Postman ou Swagger Editor.


## 🔐 Autenticação & Autorização

Alguns endpoints exigem cabeçalho:

```http
x-user-role: manager
```

> Necessário para acessar relatórios de desempenho.

---

## 👤 Usuários

### Criar usuário
`POST /users`

```json
{
  "username": "ana"
}
```

### Resposta
```json
{
  "id": "guid-gerado",
  "username": "ana"
}
```

---

## 📁 Projetos

### Criar projeto
`POST /users/{userId}/projects`

```json
"Project Alpha"
```

### Listar projetos do usuário
`GET /users/{userId}/projects`

### Deletar projeto
`DELETE /projects/{projectId}`

> ❌ Não pode ser removido se houver tarefas pendentes.

---

## ✅ Tarefas

### Criar tarefa
`POST /projects/{projectId}/tasks`

```json
{
  "title": "Corrigir bug",
  "description": "Erro de null",
  "dueDate": "2025-04-30T23:59:59Z",
  "priority": "High"
}
```

> ⚠️ Regras:
- Máximo de 20 tarefas por projeto
- Prioridade imutável após criação

### Atualizar tarefa
`PUT /tasks/{taskId}`

```json
{
  "title": "Corrigir bug urgente",
  "description": "Corrigir null na produção",
  "dueDate": "2025-05-01T12:00:00Z",
  "status": "InProgress",
  "userId": "guid-usuario"
}
```

> Histórico de alterações é registrado automaticamente.

### Deletar tarefa
`DELETE /tasks/{taskId}`

---

## 💬 Comentários

### Adicionar comentário
`POST /tasks/{taskId}/comments`

```json
{
  "userId": "guid-usuario",
  "content": "Essa tarefa está quase pronta!"
}
```

---

## 🕓 Histórico da tarefa

### Listar alterações e comentários
`GET /tasks/{taskId}/history`

Retorna todos os eventos registrados da tarefa.

---

## 📊 Relatório de desempenho

### Tarefas concluídas por usuário nos últimos 30 dias
`GET /reports/performance`

🔒 **Requer cabeçalho**:
```http
x-user-role: manager
```

### Exemplo de resposta
```json
[
  {
    "id": "guid-usuario",
    "username": "ana",
    "completedTasksLast30Days": 5
  }
]
```
