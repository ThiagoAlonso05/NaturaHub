# User Stories — NaturaHub

> Formato: **Como** [persona], **quero** [ação], **para que** [benefício]  
> Critérios de aceite seguem o padrão **Given / When / Then**

---

## Sprint 1 — Cadastro de Produtos

### US-001: Cadastrar produto
**Como** revendedora,  
**quero** cadastrar um produto com nome, código Natura, categoria, preço pago e preço de catálogo,  
**para que** eu tenha um registro organizado de todos os itens que trabalho.

**Critérios de aceite:**
- [ ] Campos obrigatórios: Nome, Preço Pago, Preço Catálogo, Quantidade inicial
- [ ] Campos opcionais: Código Natura, SKU, Categoria, Imagem, Observações
- [ ] Deve validar que Preço Pago > 0
- [ ] Deve validar que Quantidade >= 0
- [ ] Ao salvar, produto aparece na listagem

---

### US-002: Listar produtos
**Como** revendedora,  
**quero** ver todos os meus produtos em uma lista com foto e informações principais,  
**para que** eu possa encontrar rapidamente o que estou procurando.

**Critérios de aceite:**
- [ ] Lista exibe: imagem, nome, categoria, quantidade em estoque, preço catálogo
- [ ] Permite busca por nome ou código
- [ ] Permite filtro por categoria
- [ ] Indica visualmente produtos com estoque baixo (abaixo do mínimo)

---

### US-003: Editar produto
**Como** revendedora,  
**quero** editar as informações de um produto já cadastrado,  
**para que** eu possa atualizar preços ou corrigir dados incorretos.

**Critérios de aceite:**
- [ ] Todos os campos do cadastro são editáveis
- [ ] Alteração de preço não afeta histórico de movimentações anteriores
- [ ] Confirmação visual após salvar

---

### US-004: Arquivar produto
**Como** revendedora,  
**quero** arquivar produtos que não trabalho mais,  
**para que** minha lista fique limpa sem perder o histórico.

**Critérios de aceite:**
- [ ] Produto arquivado não aparece na listagem principal
- [ ] Existe opção de visualizar produtos arquivados
- [ ] Produto arquivado pode ser reativado

---

## Sprint 2 — Controle de Estoque

### US-005: Registrar entrada de estoque
**Como** revendedora,  
**quero** registrar uma entrada de produtos quando recebo um pedido Natura,  
**para que** meu estoque seja atualizado automaticamente.

**Critérios de aceite:**
- [ ] Deve informar: produto, quantidade, preço pago unitário, data
- [ ] Saldo do produto é atualizado imediatamente
- [ ] Entrada fica registrada no histórico de movimentações

---

### US-006: Registrar saída de estoque (venda)
**Como** revendedora,  
**quero** registrar uma venda de produto,  
**para que** meu estoque seja reduzido e o lucro calculado automaticamente.

**Critérios de aceite:**
- [ ] Deve informar: produto, quantidade, preço de venda, data
- [ ] Não permite venda com quantidade superior ao estoque atual
- [ ] Saída fica registrada no histórico

---

### US-007: Ver histórico de movimentações
**Como** revendedora,  
**quero** ver todas as entradas e saídas de um produto,  
**para que** eu entenda o fluxo daquele item ao longo do tempo.

**Critérios de aceite:**
- [ ] Lista entradas (E) e saídas (S) em ordem cronológica
- [ ] Exibe data, tipo, quantidade e preço de cada movimentação
- [ ] Mostra saldo acumulado após cada movimentação

---

## Sprint 3 — Financeiro + Dashboard

### US-008: Ver lucro por produto
**Como** revendedora,  
**quero** ver o lucro esperado e o lucro realizado de cada produto,  
**para que** eu saiba quais produtos são mais rentáveis.

**Critérios de aceite:**
- [ ] Lucro esperado = (Preço Catálogo - Preço Pago) × Quantidade em estoque
- [ ] Lucro realizado = soma do lucro nas vendas registradas
- [ ] Margem exibida em % e em R$

---

### US-009: Ver dashboard geral
**Como** revendedora,  
**quero** uma tela inicial com os principais indicadores do meu negócio,  
**para que** eu entenda a saúde do meu estoque em segundos.

**Critérios de aceite:**
- [ ] Card: Valor total investido em estoque
- [ ] Card: Lucro potencial total (se vender tudo)
- [ ] Card: Total de produtos cadastrados
- [ ] Card: Produtos com estoque abaixo do mínimo
- [ ] Lista: Top 5 produtos com maior margem
- [ ] Lista: Produtos sem movimentação nos últimos 30 dias (parados)
