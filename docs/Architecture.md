# Arquitetura — NaturaHub

## Visão Geral

O NaturaHub segue uma arquitetura **cliente-servidor desacoplada**, com frontend e backend independentes comunicando-se via REST API.

```
┌─────────────────────┐          HTTPS/JSON           ┌──────────────────────────┐
│                     │ ─────────────────────────────>│                          │
│   React + Vite      │                               │   ASP.NET Core Web API   │
│   (Frontend SPA)    │ <─────────────────────────────│   (.NET 9)               │
│                     │                               │                          │
└─────────────────────┘                               └──────────┬───────────────┘
        │                                                        │
   Vercel                                              Railway / Render
                                                                 │
                                                    ┌────────────▼───────────────┐
                                                    │                            │
                                                    │   SQL Server               │
                                                    │   (Entity Framework Core)  │
                                                    │                            │
                                                    └────────────────────────────┘
```

---

## Backend — ASP.NET Core Web API

### Padrões Adotados

| Padrão | Motivo |
|--------|--------|
| **Repository Pattern** | Desacopla acesso a dados da lógica de negócio |
| **Service Layer** | Centraliza regras de negócio, mantém controllers enxutos |
| **DTOs** | Evita exposição de entidades do banco diretamente na API |
| **Result Pattern** | Tratamento de erros padronizado sem exceptions desnecessárias |

### Estrutura de Pastas (Backend)

```
NaturaHub.API/
├── Controllers/          # Endpoints da API
├── Services/             # Lógica de negócio
│   └── Interfaces/
├── Repositories/         # Acesso a dados
│   └── Interfaces/
├── Models/               # Entidades do banco (EF Core)
├── DTOs/                 # Objetos de transferência de dados
│   ├── Requests/
│   └── Responses/
├── Data/                 # DbContext e Migrations
├── Mappings/             # AutoMapper profiles
└── Common/               # Result pattern, helpers
```

### Convenções de API

- Versionamento via URL: `/api/v1/`
- Resposta padronizada com envelope:
```json
{
  "success": true,
  "data": { },
  "message": "string",
  "errors": []
}
```

---

## Frontend — React + Vite

### Estrutura de Pastas (Frontend)

```
src/
├── components/           # Componentes reutilizáveis
│   └── ui/               # Shadcn/ui base components
├── pages/                # Páginas da aplicação
├── hooks/                # Custom hooks (React Query)
├── services/             # Chamadas à API (Axios)
├── types/                # TypeScript types e interfaces
├── utils/                # Funções utilitárias
└── contexts/             # Contextos React (auth, theme)
```

### Gerenciamento de Estado

| Tipo de estado | Solução |
|---------------|---------|
| Estado de servidor (API) | React Query (TanStack Query) |
| Estado global UI | React Context |
| Estado de formulário | React Hook Form |
| Estado local | useState / useReducer |

---

## Decisões Técnicas

### Por que React + Vite e não Blazor?
- React é a skill frontend mais demandada no mercado
- Arquitetura desacoplada demonstra maturidade técnica
- Permite evolução independente de frontend e backend

### Por que SQL Server e não PostgreSQL?
- Stack natural do ecossistema .NET/Azure
- Perfeita integração com Entity Framework Core
- Facilita a evolução para Azure SQL no futuro

### Por que Railway/Render e não Azure desde o início?
- Menor custo e complexidade no MVP
- Migração para Azure será uma sprint dedicada (Sprint 9)
- Foco no produto, não na infra no início
