# API Reference — NaturaHub

> Base URL: `https://api.naturahub.com/api/v1`  
> Autenticação: Bearer Token (JWT) — Sprint 4  
> Content-Type: `application/json`

---

## Envelope de Resposta Padrão

```json
{
  "success": true,
  "data": { },
  "message": "Operação realizada com sucesso",
  "errors": []
}
```

---

## Categories

### `GET /categories`
Lista todas as categorias.

**Response 200:**
```json
{
  "success": true,
  "data": [
    { "id": "uuid", "name": "Perfumaria", "description": "..." }
  ]
}
```

---

## Products

### `GET /products`
Lista produtos com suporte a filtros e paginação.

**Query Params:**

| Param | Tipo | Descrição |
|-------|------|-----------|
| `search` | string | Busca por nome ou código Natura |
| `categoryId` | uuid | Filtra por categoria |
| `isActive` | bool | Filtra ativos/arquivados (default: true) |
| `page` | int | Página (default: 1) |
| `pageSize` | int | Itens por página (default: 20) |

**Response 200:**
```json
{
  "success": true,
  "data": {
    "items": [
      {
        "id": "uuid",
        "name": "Natura Homem",
        "naturaCode": "12345",
        "category": { "id": "uuid", "name": "Perfumaria" },
        "imageUrl": "https://...",
        "purchasePrice": 45.90,
        "catalogPrice": 89.90,
        "currentStock": 5,
        "minimumStock": 2,
        "isLowStock": false
      }
    ],
    "totalCount": 42,
    "page": 1,
    "pageSize": 20
  }
}
```

---

### `POST /products`
Cadastra um novo produto.

**Request Body:**
```json
{
  "name": "Natura Homem",
  "naturaCode": "12345",
  "sku": "NH-001",
  "categoryId": "uuid",
  "imageUrl": "https://...",
  "purchasePrice": 45.90,
  "catalogPrice": 89.90,
  "initialQuantity": 3,
  "minimumQuantity": 1,
  "description": "Observações sobre o produto"
}
```

**Response 201:**
```json
{
  "success": true,
  "data": { "id": "uuid" },
  "message": "Produto cadastrado com sucesso"
}
```

---

### `GET /products/{id}`
Retorna detalhes de um produto específico.

### `PUT /products/{id}`
Atualiza um produto existente (mesmo body do POST).

### `PATCH /products/{id}/archive`
Arquiva um produto.

### `PATCH /products/{id}/restore`
Restaura um produto arquivado.

---

## Stock Movements

### `GET /products/{id}/movements`
Retorna o histórico de movimentações de um produto.

**Response 200:**
```json
{
  "success": true,
  "data": {
    "currentStock": 5,
    "movements": [
      {
        "id": "uuid",
        "type": "IN",
        "quantity": 3,
        "unitPrice": 45.90,
        "notes": "Pedido ciclo 10",
        "movementDate": "2025-05-01T00:00:00Z"
      }
    ]
  }
}
```

---

### `POST /products/{id}/movements`
Registra uma movimentação (entrada ou saída).

**Request Body:**
```json
{
  "type": "IN",
  "quantity": 5,
  "unitPrice": 45.90,
  "notes": "Pedido ciclo 10",
  "movementDate": "2025-05-01"
}
```

> `type`: `"IN"` para entrada, `"OUT"` para saída (venda)

---

## Dashboard

### `GET /dashboard`
Retorna todos os indicadores do dashboard.

**Response 200:**
```json
{
  "success": true,
  "data": {
    "totalInvested": 1250.00,
    "totalExpectedProfit": 2100.00,
    "totalRealizedProfit": 850.00,
    "totalProducts": 24,
    "lowStockProducts": 3,
    "topProfitableProducts": [ ],
    "inactiveProducts": [ ]
  }
}
```

---

## Códigos de Erro

| Código | Significado |
|--------|------------|
| 400 | Bad Request — dados inválidos |
| 404 | Not Found — recurso não encontrado |
| 409 | Conflict — regra de negócio violada |
| 422 | Unprocessable Entity — validação falhou |
| 500 | Internal Server Error |
