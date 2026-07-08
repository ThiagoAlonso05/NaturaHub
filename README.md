# 🌿 NaturaHub

> Plataforma completa de gestão para revendedores Natura.

![Status](https://img.shields.io/badge/status-em%20desenvolvimento-yellow)
![.NET](https://img.shields.io/badge/.NET-9.0-purple)
![React](https://img.shields.io/badge/React-19-blue)
![License](https://img.shields.io/badge/license-MIT-green)

---

## 💡 Sobre o Projeto

O **NaturaHub** nasceu de uma necessidade real: revendedores Natura precisam de uma ferramenta para controlar estoque, calcular margem de lucro e entender o desempenho dos seus produtos — tudo em um só lugar.

Este projeto foi desenvolvido com mentalidade de produto, passando por Discovery, definição de Personas, User Stories, modelagem de domínio e arquitetura antes de qualquer linha de código.

---

## ✨ Funcionalidades (MVP)

- 📦 **Cadastro de Produtos** — nome, SKU, código Natura, categoria, imagem, preços
- 📊 **Controle de Estoque** — entradas, saídas e histórico de movimentações
- 💰 **Módulo Financeiro** — lucro esperado, lucro realizado, margem por produto
- 📈 **Dashboard** — visão geral do negócio, alertas e indicadores principais

---

## 🛠️ Stack Técnica

### Backend
- **C# / .NET 9** — ASP.NET Core Web API
- **Entity Framework Core 9** — ORM
- **SQL Server** — Banco de dados relacional
- **JWT** — Autenticação (Sprint 4)

### Frontend
- **React 19 + Vite** — SPA moderna e performática
- **TypeScript** — Tipagem estática
- **React Query** — Gerenciamento de estado de servidor
- **Recharts** — Visualização de dados

### Infraestrutura
- **Vercel** — Deploy do frontend
- **Railway / Render** — Deploy do backend
- **GitHub Actions** — CI/CD (Sprint 9)

---

## 📁 Estrutura do Repositório

```
NaturaHub/
├── /docs               # Documentação de produto e arquitetura
│   ├── Discovery.md
│   ├── Personas.md
│   ├── UserStories.md
│   ├── Roadmap.md
│   ├── Architecture.md
│   ├── Database.md
│   └── API.md
├── /backend            # ASP.NET Core Web API
├── /frontend           # React + Vite + TypeScript
└── README.md
```

---

## 🗺️ Roadmap

| Sprint | Feature | Status |
|--------|---------|--------|
| 1 | Cadastro de Produtos | 🔄 Em andamento |
| 2 | Controle de Estoque | ⏳ Planejado |
| 3 | Financeiro + Dashboard | ⏳ Planejado |
| 4 | Autenticação JWT | ⏳ Planejado |
| 5 | Alertas | ⏳ Planejado |
| 6 | Módulo de Clientes | ⏳ Planejado |
| 7 | Pedidos e Histórico | ⏳ Planejado |
| 8 | Multiusuário + Permissões | ⏳ Planejado |
| 9 | Docker + CI/CD + Azure | ⏳ Planejado |
| 10 | Testes | ⏳ Planejado |
| 11 | Observabilidade | ⏳ Planejado |

---

## 📄 Documentação

Toda a documentação de produto e arquitetura está disponível na pasta [`/docs`](./docs).

---

## 👤 Autor

**Thiago Alonso**  
Software Engineer + Product Owner  
[GitHub](https://github.com/ThiagoAlonso05)

---

## 📝 Licença

Este projeto está sob a licença MIT.
