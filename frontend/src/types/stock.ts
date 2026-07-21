export interface StockMovement {
  id: number;
  productId: number;
  productName?: string;
  quantity: number;
  movementType: string; // O Backend retorna "Entrada" ou "Saída"
  unitPrice: number;
  totalValue: number;
  notes?: string;
  movementDate: string;
}

export interface CreateStockMovementRequest {
  productId: number;
  quantity: number;
  movementType: number; // 1 = In, 2 = Out (Enum C#)
  unitPrice: number;
  notes?: string;
}
