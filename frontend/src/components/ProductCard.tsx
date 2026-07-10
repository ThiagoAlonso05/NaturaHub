import { Package } from 'lucide-react';
import type { Product } from '../types/product';
import { Link } from 'react-router-dom';

interface ProductCardProps {
  product: Product;
}

export function ProductCard({ product }: ProductCardProps) {
  return (
    <div style={{
      background: 'var(--bg-primary)',
      borderRadius: 'var(--radius-card)',
      boxShadow: 'var(--shadow-sm)',
      padding: '16px',
      display: 'flex',
      flexDirection: 'column',
      gap: '12px',
      border: '1px solid var(--bg-secondary)',
      transition: 'box-shadow 0.2s ease'
    }}
    onMouseEnter={(e) => e.currentTarget.style.boxShadow = 'var(--shadow-md)'}
    onMouseLeave={(e) => e.currentTarget.style.boxShadow = 'var(--shadow-sm)'}
    >
      {/* Imagem Placeholder */}
      <div style={{
        width: '100%',
        height: '150px',
        background: 'var(--bg-secondary)',
        borderRadius: '4px',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        color: 'var(--text-secondary)'
      }}>
        <Package size={48} opacity={0.2} />
      </div>

      {/* Dados do Produto */}
      <div style={{ display: 'flex', flexDirection: 'column', gap: '4px', flex: 1 }}>
        <h3 style={{ fontSize: '16px', color: 'var(--text-primary)', margin: 0 }}>
          {product.name}
        </h3>
        {product.naturaCode && (
          <span style={{ fontSize: '12px', color: 'var(--text-secondary)' }}>Cód: {product.naturaCode}</span>
        )}
      </div>

      {/* Rodapé do Cartão */}
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginTop: 'auto' }}>
        <div style={{ display: 'flex', flexDirection: 'column' }}>
          <span style={{ fontSize: '12px', color: 'var(--text-secondary)' }}>Estoque</span>
          <span style={{ 
            fontSize: '14px', 
            fontWeight: '700',
            color: product.stock?.currentQuantity > 0 ? 'var(--text-primary)' : '#ef4444' 
          }}>
            {product.stock?.currentQuantity || 0} un
          </span>
        </div>
        
        <Link 
          to={`/stock?productId=${product.id}`}
          style={{
            background: 'var(--primary-500)',
            color: 'var(--text-inverted)',
            padding: '8px 16px',
            borderRadius: 'var(--radius-pill)',
            fontSize: '14px',
            fontWeight: '500',
            border: 'none',
            display: 'inline-flex',
            alignItems: 'center',
            justifyContent: 'center'
          }}
        >
          Mover Estoque
        </Link>
      </div>
    </div>
  );
}
