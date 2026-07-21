import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';
import Dashboard from './pages/Dashboard';
import Products from './pages/Products';
import NewProduct from './pages/NewProduct';
import Stock from './pages/Stock';

function App() {
  return (
    <BrowserRouter>
      {/* Barra superior Oficial Natura */}
      <nav style={{ padding: '20px', background: 'var(--bg-dark)', color: 'var(--text-inverted)' }}>
        <Link to="/" style={{ color: 'var(--primary-500)', marginRight: '20px', fontWeight: '900', fontSize: '20px' }}>NaturaHub</Link>
        <Link to="/" style={{ color: 'var(--text-inverted)', marginRight: '15px', fontWeight: '500' }}>Dashboard</Link>
        <Link to="/products" style={{ color: 'var(--text-inverted)', marginRight: '15px', fontWeight: '500' }}>Produtos</Link>
        <Link to="/stock" style={{ color: 'var(--text-inverted)', fontWeight: '500' }}>Estoque</Link>
      </nav>

      <main className="container" style={{ marginTop: '40px' }}>
        <Routes>
          <Route path="/" element={<Dashboard />} />
          <Route path="/products" element={<Products />} />
          <Route path="/products/new" element={<NewProduct />} />
          <Route path="/stock" element={<Stock />} />
        </Routes>
      </main>
    </BrowserRouter>
  );
}

export default App;
