# 📝 TaskManager API (.NET 9)

API para gerenciamento de tarefas e projetos com suporte a múltiplos usuários, escrita com .NET 9, PostgreSQL e Docker.

---

## 🚀 Tecnologias

- ✅ .NET 9 com Minimal API
- ✅ PostgreSQL
- ✅ Docker + Docker Compose
- ✅ CQRS (sem MediatR)
- ✅ AutoMapeamento com `implicit`/`explicit` operator
- ✅ FluentValidation
- ✅ Serilog
- ✅ Testes automatizados (unitários + integração)

---

## ✅ Regras de Negócio Implementadas

1. **Prioridades de Tarefas**
   - Cada tarefa deve ter uma prioridade atribuída (baixa, média, alta)
   - A prioridade **não pode ser alterada** após a criação

2. **Restrições de Remoção de Projetos**
   - Projetos com tarefas pendentes **não podem ser removidos**
   - A API retorna erro informando a necessidade de concluir ou excluir as tarefas

3. **Histórico de Atualizações**
   - Cada alteração em uma tarefa registra um histórico com:
     - Campo alterado
     - Valor anterior e novo valor
     - Data da modificação
     - Usuário responsável

4. **Limite de Tarefas por Projeto**
   - Cada projeto pode conter no máximo **20 tarefas**
   - Tentativas de adicionar além do limite resultam em erro

5. **Relatórios de Desempenho**
   - Endpoint com estatísticas de tarefas concluídas nos últimos 30 dias por usuário
   - Acesso **restrito a usuários com papel "Gerente"**

6. **Comentários nas Tarefas**
   - Comentários podem ser adicionados a qualquer tarefa
   - Comentários são registrados também no histórico da tarefa

---

## 🐳 Como executar com Docker

### 🔧 Build e subida dos containers

```bash
docker compose up -d --build
```

A aplicação estará disponível em:

👉 http://localhost:5000/swagger

---

## 🧪 Resetando o ambiente (limpa volumes e recompila)

```bash
docker compose down -v
docker compose up --build --no-cache -d
```

---

## 🧠 Sobre o Banco de Dados

- Utiliza PostgreSQL via Docker (imagem oficial)
- Volume persistente chamado `pgdata`
- Script `wait-for-postgres.sh` aguarda o banco ficar pronto antes de iniciar a API

---

### ⚙️ Execução condicional de migrations

- Em **produção** (Docker), o projeto aplica migrations automaticamente
- Em **desenvolvimento/local**, o projeto **não executa migrations automaticamente**

Você pode configurar isso no `appsettings.json` ou checar o ambiente `IHostEnvironment`.

---

## 📂 Estrutura de Projeto

- `TaskManager.Api` – Projeto de API (Minimal API, Swagger, Middlewares)
- `TaskManager.Application` – Casos de uso, validações e handlers CQRS
- `TaskManager.Core` – Entidades, interfaces e contratos
- `TaskManager.Infrastructure` – Repositórios EF Core, migrations, contexto
- `TaskManager.Tests` – Testes unitários
- `TaskManager.Tests.Integration` – Testes com WebApplicationFactory e banco real via Docker

---

## 🧪 Testes

### Cobertura mínima exigida: 80%

Para gerar relatório de cobertura:
```bash
dotnet clean
Remove-Item -Recurse -Force .\coverage-report
Get-ChildItem -Path . -Filter coverage.cobertura.xml -Recurse | Remove-Item -Force
dotnet test --collect:"XPlat Code Coverage"
reportgenerator -reports:**/coverage.cobertura.xml -targetdir:coverage-report -reporttypes:Html
```

---

## 🐘 Acessando o banco PostgreSQL via DBeaver

- Host: `localhost`
- Porta: `5432`
- Banco: `taskdb`
- Usuário: `postgres`
- Senha: `postgres`

---

## 📌 Observações

- **MediatR** e **AutoMapper** foram substituídos por implementações próprias para evitar dependência de pacotes comerciais.
- O projeto é modular e segue boas práticas de DDD e SOLID.

---

### ✅ Geração de relatório de cobertura de testes

Para garantir a precisão do relatório e evitar conflitos com arquivos antigos:

```powershell
dotnet clean
Remove-Item -Recurse -Force .\coverage-report
Get-ChildItem -Path . -Filter coverage.cobertura.xml -Recurse | Remove-Item -Force
dotnet test --collect:"XPlat Code Coverage"
reportgenerator -reports:**/coverage.cobertura.xml -targetdir:coverage-report -reporttypes:Html
```

📁 O relatório será gerado na pasta `coverage-report/index.html` (o relatório vai ficar na pasta que foi executado os comandos). Basta abrir esse arquivo no navegador para visualizar a cobertura de testes da aplicação.
