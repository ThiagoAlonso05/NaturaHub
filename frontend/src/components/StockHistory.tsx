import type { StockMovement } from '../types/stock';
import { ArrowDownRight, ArrowUpRight } from 'lucide-react';

interface StockHistoryProps {
  movements: StockMovement[];
  loading?: boolean;
}

export function StockHistory({ movements, loading = false }: StockHistoryProps) {
  if (loading) {
    return <div style={{ color: 'var(--text-secondary)', padding: '20px', textAlign: 'center' }}>Carregando histórico...</div>;
  }

  if (movements.length === 0) {
    return (
      <div style={{ color: 'var(--text-secondary)', padding: '40px 20px', textAlign: 'center', background: 'var(--bg-secondary)', borderRadius: '8px' }}>
        Nenhuma movimentação registrada para este produto.
      </div>
    );
  }

  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: '12px' }}>
      {movements.map((mov) => {
        const isEntry = mov.movementType === 'Entrada';
        return (
          <div key={mov.id} style={{
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'space-between',
            padding: '16px',
            background: 'var(--bg-primary)',
            borderRadius: 'var(--radius-card)',
            borderLeft: `4px solid ${isEntry ? '#10b981' : '#f97316'}`,
            boxShadow: 'var(--shadow-sm)'
          }}>
            <div style={{ display: 'flex', alignItems: 'center', gap: '12px' }}>
              <div style={{
                background: isEntry ? '#d1fae5' : '#ffedd5',
                color: isEntry ? '#059669' : '#ea580c',
                padding: '8px',
                borderRadius: '50%',
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center'
              }}>
                {isEntry ? <ArrowDownRight size={20} /> : <ArrowUpRight size={20} />}
              </div>
              <div style={{ display: 'flex', flexDirection: 'column' }}>
                <span style={{ fontWeight: '500', color: 'var(--text-primary)' }}>
                  {isEntry ? 'Entrada' : 'Saída'} 
                  {mov.productName ? ` - ${mov.productName}` : ''}
                  {mov.notes ? ` - ${mov.notes}` : ''}
                </span>
                <span style={{ fontSize: '12px', color: 'var(--text-secondary)' }}>
                  {new Date(mov.movementDate).toLocaleString('pt-BR')}
                </span>
              </div>
            </div>
            
            <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'flex-end' }}>
              <div style={{ 
                fontWeight: 'bold', 
                fontSize: '16px',
                color: isEntry ? '#059669' : '#ea580c'
              }}>
                {isEntry ? '+' : '-'}{mov.quantity} un
              </div>
              <div style={{ fontSize: '12px', color: 'var(--text-secondary)' }}>
                {mov.unitPrice?.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })} un
              </div>
              <div style={{ fontSize: '12px', fontWeight: 'bold', color: 'var(--text-primary)', marginTop: '2px' }}>
                Total: {mov.totalValue?.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}
              </div>
            </div>
          </div>
        );
      })}
    </div>
  );
}
