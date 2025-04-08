# 🧠 Task Manager API (.NET 9 + Minimal API + CQRS)

API de gerenciamento de tarefas desenvolvida com foco em boas práticas, SOLID e arquitetura limpa.  
Utiliza **.NET 9 Preview**, **Minimal API**, **CQRS próprio** e **operadores `explicit`/`implicit` para mapeamento entre DTOs e entidades**.

---

## 🚀 Tecnologias utilizadas

- [.NET 9 Preview](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- Minimal API
- CQRS (implementado manualmente)
- FluentValidation (a ser integrado)
- Swagger (Swashbuckle)
- Injeção de dependência nativa
- InMemory Repository (por enquanto)

---

## 📁 Estrutura Geral do Projeto

```
task-manager-api-dotnet9/
├── TaskManager.sln                   # Solução principal
├── src/
│   ├── TaskManager.Core/             # Entidades de domínio e contratos
│   ├── TaskManager.Application/      # Camada de aplicação (DTOs, CQRS, Handlers)
│   ├── TaskManager.Infrastructure/   # Acesso a dados (repositórios)
│   └── TaskManager.Api/              # Ponto de entrada da aplicação (Minimal API)
```

### 📦 `TaskManager.Core`

👉 **Responsável pelas entidades de domínio e contratos.**  
Aqui fica a base do modelo, sem dependências de infraestrutura.

**Principais arquivos:**
- `Entities/User.cs`  
- `Entities/Project.cs`  
- `Entities/TaskItem.cs`  
- `Interfaces/IUserRepository.cs`

> 💡 Esta camada só contém lógicas puras do domínio.

### 📦 `TaskManager.Application`

👉 **Camada de aplicação:** orquestra o fluxo entre os dados e a lógica de domínio.  
Aqui ficam os DTOs, os handlers de comandos e consultas.

**Principais arquivos:**
- `DTOs/UserDto.cs`  
- `DTOs/CreateUserDto.cs`  
- `Commands/CreateUserCommand.cs`  
- `Handlers/CreateUserHandler.cs`

> 💡 Aqui aplicamos **CQRS próprio** com classes simples (`Command`, `Handler`).  
> Também colocamos os **operadores `explicit`** para conversão entre DTO ↔ entidade.

### 📦 `TaskManager.Infrastructure`

👉 **Camada de infraestrutura e persistência.**

**Arquivo presente:**
- `Data/InMemoryUserRepository.cs`  
Implementa o contrato `IUserRepository` da camada `Core`, usando uma lista na memória.

> 💡 Aqui no futuro você pode colocar o `DbContext` com EF Core (usando PostgreSQL ou SQLite, por exemplo).

### 📦 `TaskManager.Api`

👉 **Camada de API com Minimal API (.NET 9).**  
Responsável pelos endpoints HTTP.

**Arquivo presente:**
- `Program.cs`

Contém:
- Configuração de DI (repositório, handler)
- Endpoint: `POST /users` para criação de usuário
- Uso de `CreateUserHandler` para seguir o padrão CQRS
- Swagger ativado para teste

---

## 🧠 Resumo visual do fluxo

```
[API (Program.cs)] 
    ↳ recebe DTO
        ↳ instancia Command
            ↳ chama Handler
                ↳ usa Repositório (via DI)
                    ↳ salva entidade
        ↳ converte para DTO (via `explicit`)
    ↳ retorna resultado
```

---

## 🛠️ Requisitos

- [.NET 9 SDK (Preview)](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- IDE: Visual Studio 2022 17.9+, Rider ou VS Code

---

## 🧪 Como rodar localmente

1. **Clone o repositório**

```bash
git clone https://github.com/seu-usuario/task-manager-api-dotnet9.git
cd task-manager-api-dotnet9
```

2. **Restaure os pacotes**

```bash
dotnet restore
```

3. **Rode o projeto**

```bash
dotnet run --project ./src/TaskManager.Api
```

4. **Acesse o Swagger para testar**

Abra no navegador:
```
http://localhost:5000/swagger
```

---

## 📌 Endpoints disponíveis

### ✅ Criar usuário

`POST /users`

```json
{
  "username": "user"
}
```

**Resposta:**

```json
{
  "id": "uuid-gerado",
  "username": "user"
}
```

---

## 🔮 Próximos passos

- Adicionar endpoints para `Project` e `TaskItem`
- Implementar banco de dados real (SQLite ou PostgreSQL via EF Core)
- Aplicar validações com FluentValidation
- Melhorar sistema de erros
- Criar testes automatizados

---

## 📄 Licença

MIT © Felipe Ogata
