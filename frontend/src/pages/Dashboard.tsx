import { useEffect, useState } from 'react';
import { Package, Boxes, DollarSign, TrendingUp, AlertTriangle } from 'lucide-react';
import type { Product } from '../types/product';
import type { StockMovement } from '../types/stock';
import { productService } from '../services/productService';
import { stockService } from '../services/stockService';
import { StockHistory } from '../components/StockHistory';
import { Link } from 'react-router-dom';

export default function Dashboard() {
  const [products, setProducts] = useState<Product[]>([]);
  const [history, setHistory] = useState<StockMovement[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    async function loadData() {
      try {
        const [prods, hist] = await Promise.all([
          productService.getProducts(),
          stockService.getAllHistory()
        ]);
        setProducts(prods);
        setHistory(hist);
      } catch (error) {
        console.error("Erro ao carregar dados do dashboard:", error);
      } finally {
        setLoading(false);
      }
    }
    loadData();
  }, []);

  // Compute KPIs
  const totalProducts = products.length;
  const totalStockItems = products.reduce((sum, p) => sum + (p.currentStockQuantity || 0), 0);
  const potentialRevenue = products.reduce((sum, p) => sum + ((p.currentStockQuantity || 0) * (p.catalogPrice || 0)), 0);
  
  // Total Inflows (Entradas no mês)
  const currentMonth = new Date().getMonth();
  const currentYear = new Date().getFullYear();
  
  const monthlyInflows = history
    .filter(m => {
      const d = new Date(m.movementDate);
      return m.movementType === 'Entrada' && d.getMonth() === currentMonth && d.getFullYear() === currentYear;
    })
    .reduce((sum, m) => sum + (m.totalValue || 0), 0);

  // Low Stock Alerts
  const lowStockProducts = products.filter(p => p.currentStockQuantity > 0 && p.currentStockQuantity <= 5);
  const outOfStockProducts = products.filter(p => p.currentStockQuantity === 0);

  if (loading) {
    return (
      <div className="fade-in" style={{ padding: '40px', textAlign: 'center', color: 'var(--text-secondary)' }}>
        <p>Carregando seu painel de controle executivo...</p>
      </div>
    );
  }

  const kpiCardStyle = {
    background: 'var(--bg-primary)',
    padding: '24px',
    borderRadius: 'var(--radius-card)',
    boxShadow: 'var(--shadow-sm)',
    display: 'flex',
    alignItems: 'center',
    gap: '16px'
  };

  const iconContainerStyle = (bgColor: string, color: string) => ({
    background: bgColor,
    color: color,
    width: '48px',
    height: '48px',
    borderRadius: '50%',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center'
  });

  return (
    <div className="fade-in">
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '24px' }}>
        <div>
          <h1 style={{ fontSize: '24px', color: 'var(--text-primary)', fontWeight: 'bold' }}>Visão Geral</h1>
          <p style={{ color: 'var(--text-secondary)', marginTop: '4px' }}>Acompanhe os principais indicadores do seu estoque.</p>
        </div>
        
        <Link to="/stock" style={{
          background: 'var(--primary-500)',
          color: 'var(--text-inverted)',
          textDecoration: 'none',
          padding: '10px 24px',
          borderRadius: '999px',
          fontWeight: '600',
          display: 'flex',
          alignItems: 'center',
          gap: '8px',
          boxShadow: '0 4px 6px -1px rgba(234, 88, 12, 0.2)'
        }}>
          Nova Movimentação
        </Link>
      </div>

      {/* KPI GRID */}
      <div style={{ 
        display: 'grid', 
        gridTemplateColumns: 'repeat(auto-fit, minmax(240px, 1fr))', 
        gap: '20px',
        marginBottom: '32px'
      }}>
        <div style={kpiCardStyle}>
          <div style={iconContainerStyle('#ffedd5', '#ea580c')}>
            <Package size={24} />
          </div>
          <div>
            <p style={{ fontSize: '13px', color: 'var(--text-secondary)', fontWeight: '500' }}>Total de Produtos</p>
            <p style={{ fontSize: '24px', fontWeight: 'bold', color: 'var(--text-primary)' }}>{totalProducts}</p>
          </div>
        </div>

        <div style={kpiCardStyle}>
          <div style={iconContainerStyle('#d1fae5', '#059669')}>
            <Boxes size={24} />
          </div>
          <div>
            <p style={{ fontSize: '13px', color: 'var(--text-secondary)', fontWeight: '500' }}>Itens Físicos</p>
            <p style={{ fontSize: '24px', fontWeight: 'bold', color: 'var(--text-primary)' }}>{totalStockItems}</p>
          </div>
        </div>

        <div style={kpiCardStyle}>
          <div style={iconContainerStyle('#e0e7ff', '#4f46e5')}>
            <DollarSign size={24} />
          </div>
          <div>
            <p style={{ fontSize: '13px', color: 'var(--text-secondary)', fontWeight: '500' }}>Potencial de Venda</p>
            <p style={{ fontSize: '24px', fontWeight: 'bold', color: 'var(--text-primary)' }}>
              {potentialRevenue.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}
            </p>
          </div>
        </div>

        <div style={kpiCardStyle}>
          <div style={iconContainerStyle('#fce7f3', '#db2777')}>
            <TrendingUp size={24} />
          </div>
          <div>
            <p style={{ fontSize: '13px', color: 'var(--text-secondary)', fontWeight: '500' }}>Entradas no Mês</p>
            <p style={{ fontSize: '24px', fontWeight: 'bold', color: 'var(--text-primary)' }}>
              {monthlyInflows.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}
            </p>
          </div>
        </div>
      </div>

      {/* BOTTOM COLUMNS */}
      <div style={{ display: 'flex', gap: '24px', flexWrap: 'wrap' }}>
        
        {/* LEFT: HISTÓRICO */}
        <div style={{ flex: '2 1 400px' }}>
          <div style={{ 
            background: 'var(--bg-primary)', 
            padding: '24px', 
            borderRadius: 'var(--radius-card)',
            boxShadow: 'var(--shadow-sm)',
            height: '100%'
          }}>
            <h2 style={{ fontSize: '18px', fontWeight: 'bold', marginBottom: '20px', color: 'var(--text-primary)' }}>
              Últimas Movimentações
            </h2>
            
            {history.length === 0 ? (
              <p style={{ color: 'var(--text-secondary)' }}>Nenhuma movimentação registrada.</p>
            ) : (
              <>
                <StockHistory movements={history.slice(0, 5)} loading={false} />
                <div style={{ marginTop: '20px', textAlign: 'center' }}>
                  <Link to="/stock" style={{ color: 'var(--primary-500)', textDecoration: 'none', fontWeight: '600', fontSize: '14px' }}>
                    Acessar histórico completo &rarr;
                  </Link>
                </div>
              </>
            )}
          </div>
        </div>

        {/* RIGHT: ALERTAS */}
        <div style={{ flex: '1 1 300px' }}>
          <div style={{ 
            background: 'var(--bg-primary)', 
            padding: '24px', 
            borderRadius: 'var(--radius-card)',
            boxShadow: 'var(--shadow-sm)',
            height: '100%'
          }}>
            <div style={{ display: 'flex', alignItems: 'center', gap: '8px', marginBottom: '20px' }}>
              <AlertTriangle size={20} color="#dc2626" />
              <h2 style={{ fontSize: '18px', fontWeight: 'bold', color: 'var(--text-primary)' }}>
                Alertas de Estoque
              </h2>
            </div>
            
            {outOfStockProducts.length === 0 && lowStockProducts.length === 0 ? (
              <div style={{ padding: '24px', textAlign: 'center', background: '#f0fdf4', borderRadius: '8px', color: '#15803d' }}>
                <p style={{ fontWeight: '500' }}>Tudo certo!</p>
                <p style={{ fontSize: '14px', marginTop: '4px' }}>Seu estoque está saudável.</p>
              </div>
            ) : (
              <div style={{ display: 'flex', flexDirection: 'column', gap: '16px' }}>
                {outOfStockProducts.map(p => (
                  <div key={p.id} style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', paddingBottom: '12px', borderBottom: '1px solid #e5e7eb' }}>
                    <span style={{ fontSize: '14px', fontWeight: '500', color: 'var(--text-primary)' }}>{p.name}</span>
                    <span style={{ fontSize: '12px', background: '#fee2e2', color: '#dc2626', padding: '4px 10px', borderRadius: '999px', fontWeight: 'bold' }}>
                      ESGOTADO
                    </span>
                  </div>
                ))}

                {lowStockProducts.map(p => (
                  <div key={p.id} style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', paddingBottom: '12px', borderBottom: '1px solid #e5e7eb' }}>
                    <span style={{ fontSize: '14px', fontWeight: '500', color: 'var(--text-primary)' }}>{p.name}</span>
                    <span style={{ fontSize: '12px', background: '#fef3c7', color: '#d97706', padding: '4px 10px', borderRadius: '999px', fontWeight: 'bold' }}>
                      RESTAM {p.currentStockQuantity}
                    </span>
                  </div>
                ))}
              </div>
            )}
          </div>
        </div>

      </div>
    </div>
  );
}
