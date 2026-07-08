# Roadmap — NaturaHub

> Cada sprint tem duração estimada de **2 semanas**.

---

## MVP — Sprints 1 a 3

### Sprint 1 — Cadastro de Produtos
**Objetivo**: Usuário consegue cadastrar, listar, editar e arquivar produtos.

| # | User Story | Prioridade |
|---|-----------|-----------|
| US-001 | Cadastrar produto | Alta |
| US-002 | Listar produtos com busca e filtro | Alta |
| US-003 | Editar produto | Alta |
| US-004 | Arquivar produto | Média |

**Definition of Done:**
- [ ] CRUD de produtos funcionando
- [ ] Upload de imagem funcionando
- [ ] Busca e filtro por categoria implementados
- [ ] Responsivo no mobile

---

### Sprint 2 — Controle de Estoque
**Objetivo**: Usuário consegue registrar entradas e saídas e ver histórico.

| # | User Story | Prioridade |
|---|-----------|-----------|
| US-005 | Registrar entrada | Alta |
| US-006 | Registrar saída (venda) | Alta |
| US-007 | Ver histórico de movimentações | Alta |

**Definition of Done:**
- [ ] Entradas e saídas registradas corretamente
- [ ] Saldo de estoque atualizado em tempo real
- [ ] Histórico ordenado por data

---

### Sprint 3 — Financeiro + Dashboard
**Objetivo**: Usuário consegue ver lucro, margem e visão geral do negócio.

| # | User Story | Prioridade |
|---|-----------|-----------|
| US-008 | Ver lucro por produto | Alta |
| US-009 | Ver dashboard geral | Alta |

**Definition of Done:**
- [ ] Cálculos financeiros corretos e validados
- [ ] Dashboard com todos os cards e listas
- [ ] Alertas de estoque baixo funcionando

---

## Pós-MVP — Sprints 4 a 11

| Sprint | Feature | Objetivo |
|--------|---------|----------|
| **4** | Autenticação (JWT + Login) | Proteger acesso aos dados |
| **5** | Alertas (estoque baixo, produtos parados) | Prevenção de perda |
| **6** | Módulo de Clientes | Registrar quem compra o quê |
| **7** | Pedidos e histórico por cliente | Relacionamento e fidelização |
| **8** | Multiusuário + Permissões | Base para modelo SaaS |
| **9** | Docker + CI/CD + Azure | Deploy profissional e automatizado |
| **10** | Testes (unitários + integração) | Qualidade e confiabilidade |
| **11** | Observabilidade (logs, métricas) | Monitoramento em produção |

---

## Tecnologias por Sprint

| Sprint | Tecnologias novas introduzidas |
|--------|-------------------------------|
| 1-3 | .NET 9, EF Core, React, Vite, TypeScript, SQL Server |
| 4 | JWT, Refresh Token, BCrypt |
| 5 | SignalR ou polling, sistema de notificações |
| 6-7 | Relacionamentos complexos no EF Core |
| 8 | Claims, Roles, Policies no ASP.NET Core |
| 9 | Docker, GitHub Actions, Azure App Service |
| 10 | xUnit, Moq, TestContainers, Cypress |
| 11 | Serilog, OpenTelemetry, Application Insights |
