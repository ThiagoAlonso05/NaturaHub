import { api } from './api';
import type { StockMovement, CreateStockMovementRequest } from '../types/stock';

export const stockService = {
  getAllHistory: async (): Promise<StockMovement[]> => {
    // Workaround no Frontend: Busca todos os produtos, depois busca o histórico de cada um e mescla.
    const productsRes = await api.get('/products');
    const products = productsRes.data;
    
    const historyPromises = products.map((p: any) => api.get(`/stockmovements/product/${p.id}`));
    const historyResponses = await Promise.all(historyPromises);
    
    // Mescla tudo em um único array
    const allMovements = historyResponses.flatMap(res => res.data);
    
    // Ordena pela data mais recente (decrescente)
    return allMovements.sort((a, b) => new Date(b.movementDate).getTime() - new Date(a.movementDate).getTime());
  },

  getHistoryByProduct: async (productId: number): Promise<StockMovement[]> => {
    const response = await api.get(`/stockmovements/product/${productId}`);
    return response.data;
  },

  createMovement: async (data: CreateStockMovementRequest): Promise<StockMovement> => {
    const response = await api.post('/stockmovements', data);
    return response.data;
  }
};
