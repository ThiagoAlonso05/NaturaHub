import { api } from './api';
import type { Product, CreateProductRequest } from '../types/product';

export const productService = {
  getProducts: async (): Promise<Product[]> => {
    const response = await api.get('/products');
    return response.data;
  },

  getProductById: async (id: number): Promise<Product> => {
    const response = await api.get(`/products/${id}`);
    return response.data;
  },

  createProduct: async (data: CreateProductRequest): Promise<Product> => {
    const response = await api.post('/products', data);
    return response.data;
  }
};
