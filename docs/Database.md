# Database — NaturaHub

## Modelo Entidade-Relacionamento (MVP)

```
┌──────────────┐       ┌─────────────┐       ┌──────────────────┐
│  Categories  │       │  Products   │       │  StockMovements  │
│──────────────│       │─────────────│       │──────────────────│
│ Id (PK)      │──────<│ Id (PK)     │──────<│ Id (PK)          │
│ Name         │       │ CategoryId  │       │ ProductId (FK)   │
│ Description  │       │ Name        │       │ MovementType     │
│ CreatedAt    │       │ NaturaCode  │       │ Quantity         │
└──────────────┘       │ SKU         │       │ UnitPrice        │
                       │ ImageUrl    │       │ Notes            │
                       │ PurchasePrc │       │ MovementDate     │
                       │ CatalogPrc  │       │ CreatedAt        │
                       │ Description │       └──────────────────┘
                       │ IsActive    │
                       │ CreatedAt   │       ┌──────────────┐
                       │ UpdatedAt   │──────<│    Stock     │
                       └─────────────┘       │──────────────│
                                             │ Id (PK)      │
                                             │ ProductId FK)│
                                             │ CurrentQty   │
                                             │ MinimumQty   │
                                             │ UpdatedAt    │
                                             └──────────────┘
```

---

## Scripts DDL

```sql
-- =============================================
-- CATEGORIES
-- =============================================
CREATE TABLE Categories (
    Id          UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    Name        NVARCHAR(100)       NOT NULL,
    Description NVARCHAR(500)       NULL,
    CreatedAt   DATETIME2           NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT PK_Categories PRIMARY KEY (Id)
);

-- =============================================
-- PRODUCTS
-- =============================================
CREATE TABLE Products (
    Id            UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    CategoryId    UNIQUEIDENTIFIER    NOT NULL,
    Name          NVARCHAR(200)       NOT NULL,
    NaturaCode    NVARCHAR(50)        NULL,
    SKU           NVARCHAR(50)        NULL,
    ImageUrl      NVARCHAR(500)       NULL,
    PurchasePrice DECIMAL(10, 2)      NOT NULL,
    CatalogPrice  DECIMAL(10, 2)      NOT NULL,
    Description   NVARCHAR(1000)      NULL,
    IsActive      BIT                 NOT NULL DEFAULT 1,
    CreatedAt     DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt     DATETIME2           NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT PK_Products      PRIMARY KEY (Id),
    CONSTRAINT FK_Products_Cat  FOREIGN KEY (CategoryId) REFERENCES Categories(Id),
    CONSTRAINT CK_PurchasePrice CHECK (PurchasePrice > 0),
    CONSTRAINT CK_CatalogPrice  CHECK (CatalogPrice > 0)
);

-- =============================================
-- STOCK
-- =============================================
CREATE TABLE Stock (
    Id              UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    ProductId       UNIQUEIDENTIFIER    NOT NULL,
    CurrentQuantity INT                 NOT NULL DEFAULT 0,
    MinimumQuantity INT                 NOT NULL DEFAULT 0,
    UpdatedAt       DATETIME2           NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT PK_Stock             PRIMARY KEY (Id),
    CONSTRAINT FK_Stock_Products    FOREIGN KEY (ProductId) REFERENCES Products(Id),
    CONSTRAINT UQ_Stock_ProductId   UNIQUE (ProductId),
    CONSTRAINT CK_CurrentQty        CHECK (CurrentQuantity >= 0),
    CONSTRAINT CK_MinimumQty        CHECK (MinimumQuantity >= 0)
);

-- =============================================
-- STOCK MOVEMENTS
-- =============================================
CREATE TABLE StockMovements (
    Id            UNIQUEIDENTIFIER    NOT NULL DEFAULT NEWSEQUENTIALID(),
    ProductId     UNIQUEIDENTIFIER    NOT NULL,
    MovementType  NVARCHAR(3)         NOT NULL,   -- 'IN' ou 'OUT'
    Quantity      INT                 NOT NULL,
    UnitPrice     DECIMAL(10, 2)      NOT NULL,
    Notes         NVARCHAR(500)       NULL,
    MovementDate  DATETIME2           NOT NULL DEFAULT GETUTCDATE(),
    CreatedAt     DATETIME2           NOT NULL DEFAULT GETUTCDATE(),

    CONSTRAINT PK_StockMovements        PRIMARY KEY (Id),
    CONSTRAINT FK_Movements_Products    FOREIGN KEY (ProductId) REFERENCES Products(Id),
    CONSTRAINT CK_MovementType          CHECK (MovementType IN ('IN', 'OUT')),
    CONSTRAINT CK_MovementQuantity      CHECK (Quantity > 0),
    CONSTRAINT CK_MovementUnitPrice     CHECK (UnitPrice > 0)
);
```

---

## Dados Iniciais (Seed)

```sql
-- Categorias padrão Natura
INSERT INTO Categories (Id, Name, Description) VALUES
(NEWID(), 'Perfumaria', 'Perfumes e colônias'),
(NEWID(), 'Maquiagem', 'Base, batom, sombra e afins'),
(NEWID(), 'Cuidados com o Rosto', 'Hidratantes, serums e tratamentos faciais'),
(NEWID(), 'Cuidados com o Corpo', 'Hidratantes corporais e esfoliantes'),
(NEWID(), 'Cuidados com o Cabelo', 'Shampoo, condicionador e tratamentos'),
(NEWID(), 'Bem-Estar', 'Suplementos e produtos de bem-estar');
```

---

## Índices Recomendados

```sql
CREATE INDEX IX_Products_CategoryId    ON Products (CategoryId);
CREATE INDEX IX_Products_IsActive      ON Products (IsActive);
CREATE INDEX IX_Movements_ProductId    ON StockMovements (ProductId);
CREATE INDEX IX_Movements_Date         ON StockMovements (MovementDate DESC);
```
