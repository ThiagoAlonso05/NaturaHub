import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { ArrowLeft, Save } from 'lucide-react';
import { productService } from '../services/productService';

export default function NewProduct() {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  // Formulário State
  const [name, setName] = useState('');
  const [naturaCode, setNaturaCode] = useState('');
  const [catalogPrice, setCatalogPrice] = useState('');
  const [categoryId, setCategoryId] = useState(1); // 1 = Perfumaria, 2 = Maquiagem (do Seed)

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    setLoading(true);
    setError('');

    try {
      await productService.createProduct({
        name,
        naturaCode,
        catalogPrice: parseFloat(catalogPrice.replace(',', '.')),
        categoryId,
        sku: `SKU-${Date.now()}` // Gerador simples provisório
      });
      
      // Volta para a tela de listagem
      navigate('/products');
    } catch (err: any) {
      console.error(err);
      setError(err.response?.data?.message || 'Erro ao criar produto. Verifique o console.');
      setLoading(false);
    }
  }

  // Estilo base para Inputs (clean)
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
    <div className="fade-in" style={{ maxWidth: '600px', margin: '0 auto' }}>
      
      <Link to="/products" style={{ display: 'inline-flex', alignItems: 'center', gap: '8px', marginBottom: '24px', color: 'var(--text-secondary)' }}>
        <ArrowLeft size={20} />
        Voltar para a Vitrine
      </Link>

      <h1 style={{ color: 'var(--text-primary)', marginBottom: '8px' }}>Novo Produto</h1>
      <p style={{ color: 'var(--text-secondary)', marginBottom: '32px' }}>Cadastre um novo item do catálogo Natura.</p>

      {error && (
        <div style={{ background: '#fee2e2', color: '#ef4444', padding: '16px', borderRadius: 'var(--radius-card)', marginBottom: '24px' }}>
          {error}
        </div>
      )}

      <form onSubmit={handleSubmit} style={{ background: 'var(--bg-primary)', padding: '32px', borderRadius: 'var(--radius-card)', boxShadow: 'var(--shadow-md)', display: 'flex', flexDirection: 'column', gap: '20px' }}>
        
        <div>
          <label style={{ fontWeight: '500' }}>Nome do Produto *</label>
          <input 
            type="text" 
            required 
            value={name}
            onChange={e => setName(e.target.value)}
            style={inputStyle} 
            placeholder="Ex: Essencial Supreme Feminino"
          />
        </div>

        <div style={{ display: 'flex', gap: '20px' }}>
          <div style={{ flex: 1 }}>
            <label style={{ fontWeight: '500' }}>Código Natura</label>
            <input 
              type="text" 
              value={naturaCode}
              onChange={e => setNaturaCode(e.target.value)}
              style={inputStyle} 
              placeholder="Ex: 81023"
            />
          </div>
          
          <div style={{ flex: 1 }}>
            <label style={{ fontWeight: '500' }}>Preço de Catálogo (R$) *</label>
            <input 
              type="number" 
              step="0.01" 
              required 
              value={catalogPrice}
              onChange={e => setCatalogPrice(e.target.value)}
              style={inputStyle} 
              placeholder="0,00"
            />
          </div>
        </div>

        <div>
          <label style={{ fontWeight: '500' }}>Categoria *</label>
          <select 
            value={categoryId} 
            onChange={e => setCategoryId(Number(e.target.value))} 
            style={inputStyle}
          >
            <option value={1}>Perfumaria</option>
            <option value={2}>Maquiagem</option>
          </select>
        </div>

        <div style={{ marginTop: '16px', display: 'flex', justifyContent: 'flex-end' }}>
          <button 
            type="submit" 
            disabled={loading}
            style={{
              background: 'var(--primary-500)',
              color: 'var(--text-inverted)',
              padding: '14px 32px',
              border: 'none',
              fontSize: '16px',
              display: 'flex',
              alignItems: 'center',
              gap: '8px',
              opacity: loading ? 0.7 : 1
            }}
          >
            <Save size={20} />
            {loading ? 'Salvando...' : 'Salvar Produto'}
          </button>
        </div>

      </form>
    </div>
  );
}
