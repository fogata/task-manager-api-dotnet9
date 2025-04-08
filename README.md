# 🧠 Task Manager API (.NET 9 + Minimal API + CQRS)

API de gerenciamento de tarefas desenvolvida com foco em boas práticas, SOLID e arquitetura limpa.  
Utiliza **.NET 9 Preview**, **Minimal API**, **CQRS próprio**, **FluentValidation**, **Serilog** e operadores `explicit`/`implicit` para mapeamento entre DTOs e entidades.

---

## 🚀 Tecnologias utilizadas

- .NET 9 Preview
- Minimal API
- CQRS Manual
- Serilog (Console + Arquivo)
- FluentValidation
- Entity Framework Core + PostgreSQL
- Swagger/OpenAPI

---

## 📁 Estrutura Geral do Projeto

(... estrutura mantida ...)

---

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
