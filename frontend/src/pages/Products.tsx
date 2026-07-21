import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { Plus } from 'lucide-react';
import type { Product } from '../types/product';
import { productService } from '../services/productService';
import { ProductCard } from '../components/ProductCard';

export default function Products() {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');

  useEffect(() => {
    async function loadProducts() {
      try {
        const data = await productService.getProducts();
        setProducts(data);
      } catch (err) {
        console.error(err);
        setError('Não foi possível carregar os produtos. O Backend C# está rodando?');
      } finally {
        setLoading(false);
      }
    }
    
    loadProducts();
  }, []);

  return (
    <div className="fade-in">
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '24px' }}>
        <div>
          <h1 style={{ color: 'var(--text-primary)' }}>Produtos</h1>
          <p style={{ color: 'var(--text-secondary)' }}>Gerencie seu catálogo de cosméticos</p>
        </div>
        
        <Link 
          to="/products/new"
          style={{
            background: 'var(--primary-500)',
            color: 'var(--text-inverted)',
            padding: '12px 24px',
            borderRadius: 'var(--radius-pill)',
            fontSize: '16px',
            fontWeight: '500',
            border: 'none',
            display: 'flex',
            alignItems: 'center',
            gap: '8px'
          }}
        >
          <Plus size={20} />
          Novo Produto
        </Link>
      </div>

      {error && (
        <div style={{ background: '#fee2e2', color: '#ef4444', padding: '16px', borderRadius: 'var(--radius-card)', marginBottom: '24px' }}>
          {error}
        </div>
      )}

      {loading ? (
        <div style={{ textAlign: 'center', padding: '40px', color: 'var(--text-secondary)' }}>
          Carregando produtos...
        </div>
      ) : products.length === 0 && !error ? (
        <div style={{ 
          textAlign: 'center', 
          padding: '60px 20px', 
          background: 'var(--bg-primary)', 
          borderRadius: 'var(--radius-card)',
          border: '1px solid var(--bg-secondary)'
        }}>
          <h3 style={{ marginBottom: '8px', color: 'var(--text-primary)' }}>Nenhum produto cadastrado</h3>
          <p style={{ color: 'var(--text-secondary)' }}>Clique em "Novo Produto" para começar a montar o seu estoque.</p>
        </div>
      ) : (
        <div style={{ 
          display: 'grid', 
          gridTemplateColumns: 'repeat(auto-fill, minmax(250px, 1fr))', 
          gap: '20px' 
        }}>
          {products.map(product => (
            <ProductCard key={product.id} product={product} />
          ))}
        </div>
      )}
    </div>
  );
}
