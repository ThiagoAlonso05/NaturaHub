import { useEffect, useState } from 'react';
import { useSearchParams } from 'react-router-dom';
import { PackagePlus, PackageMinus } from 'lucide-react';
import type { Product } from '../types/product';
import type { StockMovement } from '../types/stock';
import { productService } from '../services/productService';
import { stockService } from '../services/stockService';
import { StockHistory } from '../components/StockHistory';

export default function Stock() {
  const [searchParams] = useSearchParams();
  const urlProductId = searchParams.get('productId');

  const [products, setProducts] = useState<Product[]>([]);
  const [selectedProductId, setSelectedProductId] = useState<number | ''>(urlProductId ? parseInt(urlProductId) : '');
  const [history, setHistory] = useState<StockMovement[]>([]);
  const [loadingProducts, setLoadingProducts] = useState(true);
  const [loadingHistory, setLoadingHistory] = useState(false);
  const [submitting, setSubmitting] = useState(false);

  // Form State
  const [quantity, setQuantity] = useState('');
  const [unitPrice, setUnitPrice] = useState('');
  const [notes, setNotes] = useState('');
  const [error, setError] = useState('');
  const [successMsg, setSuccessMsg] = useState('');

  const fetchProducts = async () => {
    try {
      const data = await productService.getProducts();
      setProducts(data);
    } catch (err) {
      console.error(err);
    }
  };

  useEffect(() => {
    async function loadInitial() {
      try {
        const data = await productService.getProducts();
        setProducts(data);
        if (data.length === 1 && !selectedProductId) {
          setSelectedProductId(data[0].id);
        }
      } catch (err) {
        console.error(err);
        setError('Falha ao carregar catálogo de produtos.');
      } finally {
        setLoadingProducts(false);
      }
    }
    loadInitial();
  }, []);

  useEffect(() => {
    async function fetchHistory() {
      setLoadingHistory(true);
      try {
        if (!selectedProductId) {
          // Busca o histórico geral de todos os produtos
          const allData = await stockService.getAllHistory();
          setHistory(allData);
        } else {
          // Busca o histórico de um produto específico
          const data = await stockService.getHistoryByProduct(Number(selectedProductId));
          setHistory(data);
        }
      } catch (err) {
        console.error(err);
      } finally {
        setLoadingHistory(false);
      }
    }
    fetchHistory();
  }, [selectedProductId]);

  async function handleMovement(movementType: 'In' | 'Out') {
    if (!selectedProductId) {
      setError('Selecione um produto.');
      return;
    }
    const qty = parseInt(quantity);
    if (isNaN(qty) || qty <= 0) {
      setError('Informe uma quantidade válida maior que zero.');
      return;
    }
    const price = parseFloat(unitPrice.replace(',', '.'));
    if (isNaN(price) || price < 0) {
      setError('Informe um preço unitário válido.');
      return;
    }

    setSubmitting(true);
    setError('');
    setSuccessMsg('');

    try {
      await stockService.createMovement({
        productId: Number(selectedProductId),
        quantity: qty,
        movementType: movementType === 'In' ? 1 : 2, // 1 = In, 2 = Out no C#
        unitPrice: price,
        notes: notes || undefined
      });
      
      setSuccessMsg('Movimentação registrada com sucesso!');
      setQuantity('');
      setUnitPrice('');
      setNotes('');
      
      // Atualiza o histórico
      const updatedHistory = await stockService.getHistoryByProduct(Number(selectedProductId));
      setHistory(updatedHistory);

      // Atualiza a listagem de produtos para refrescar o total de estoque no Select
      await fetchProducts();

    } catch (err: any) {
      console.error(err);
      setError(err.response?.data?.error || 'Erro ao registrar movimentação.');
    } finally {
      setSubmitting(false);
    }
  }

  const inputStyle = {
    width: '100%',
    padding: '12px',
    borderRadius: '4px',
    border: '1px solid #ccc',
    marginTop: '6px',
    fontSize: '16px',
    fontFamily: 'inherit'
  };

  return (
    <div className="fade-in">
      <h1 style={{ color: 'var(--text-primary)', marginBottom: '8px' }}>Movimentar Estoque</h1>
      <p style={{ color: 'var(--text-secondary)', marginBottom: '32px' }}>Registre entradas de mercadoria e saídas de vendas.</p>

      {error && (
        <div style={{ background: '#fee2e2', color: '#ef4444', padding: '16px', borderRadius: 'var(--radius-card)', marginBottom: '24px' }}>
          {error}
        </div>
      )}
      
      {successMsg && (
        <div style={{ background: '#d1fae5', color: '#059669', padding: '16px', borderRadius: 'var(--radius-card)', marginBottom: '24px' }}>
          {successMsg}
        </div>
      )}

      <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '40px', alignItems: 'start' }}>
        
        {/* COLUNA ESQUERDA: FORMULÁRIO */}
        <div style={{ background: 'var(--bg-primary)', padding: '32px', borderRadius: 'var(--radius-card)', boxShadow: 'var(--shadow-md)', display: 'flex', flexDirection: 'column', gap: '20px' }}>
          
          <div>
            <label style={{ fontWeight: '500' }}>Produto *</label>
            <select 
              value={selectedProductId}
              onChange={e => setSelectedProductId(e.target.value === '' ? '' : Number(e.target.value))}
              style={inputStyle}
              disabled={loadingProducts}
            >
              <option value="">-- Selecione o Produto --</option>
              {products.map(p => (
                <option key={p.id} value={p.id}>
                  {p.name} (Atual: {p.currentStockQuantity} un)
                </option>
              ))}
            </select>
          </div>

          <div style={{ display: 'flex', gap: '20px' }}>
            <div style={{ flex: 1 }}>
              <label style={{ fontWeight: '500' }}>Quantidade *</label>
              <input 
                type="number"
                min="1"
                value={quantity}
                onChange={e => setQuantity(e.target.value)}
                style={inputStyle}
                placeholder="Ex: 10"
              />
            </div>

            <div style={{ flex: 1 }}>
              <label style={{ fontWeight: '500' }}>Preço Unitário (R$) *</label>
              <input 
                type="number"
                step="0.01"
                min="0"
                value={unitPrice}
                onChange={e => setUnitPrice(e.target.value)}
                style={inputStyle}
                placeholder="Ex: 129.90"
              />
            </div>
          </div>

          <div>
            <label style={{ fontWeight: '500' }}>Motivo / Observação (Opcional)</label>
            <input 
              type="text"
              value={notes}
              onChange={e => setNotes(e.target.value)}
              style={inputStyle}
              placeholder="Ex: Venda para Maria, Chegada de Caixa..."
            />
          </div>

          <div style={{ display: 'flex', gap: '16px', marginTop: '16px' }}>
            <button 
              onClick={() => handleMovement('In')}
              disabled={submitting}
              style={{
                flex: 1,
                background: '#10b981',
                color: 'white',
                padding: '14px',
                border: 'none',
                borderRadius: 'var(--radius-pill)',
                fontSize: '16px',
                fontWeight: '500',
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                gap: '8px',
                cursor: submitting ? 'not-allowed' : 'pointer',
                opacity: submitting ? 0.7 : 1
              }}
            >
              <PackagePlus size={20} />
              Entrada
            </button>

            <button 
              onClick={() => handleMovement('Out')}
              disabled={submitting}
              style={{
                flex: 1,
                background: '#f97316',
                color: 'white',
                padding: '14px',
                border: 'none',
                borderRadius: 'var(--radius-pill)',
                fontSize: '16px',
                fontWeight: '500',
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                gap: '8px',
                cursor: submitting ? 'not-allowed' : 'pointer',
                opacity: submitting ? 0.7 : 1
              }}
            >
              <PackageMinus size={20} />
              Saída
            </button>
          </div>

        </div>

        {/* COLUNA DIREITA: HISTÓRICO */}
        <div>
          <h2 style={{ fontSize: '18px', color: 'var(--text-primary)', marginBottom: '16px' }}>Histórico de Movimentações</h2>
          <StockHistory movements={history} loading={loadingHistory} />
        </div>

      </div>
    </div>
  );
}
