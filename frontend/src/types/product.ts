export interface Product {
  id: number;
  name: string;
  naturaCode?: string;
  sku?: string;
  categoryId: number;
  catalogPrice: number;
  imageUrl?: string;
  description?: string;
  status: string;
  currentStockQuantity: number;
}

export interface CreateProductRequest {
  name: string;
  naturaCode?: string;
  sku?: string;
  categoryId: number;
  catalogPrice: number;
  imageUrl?: string;
  description?: string;
}
