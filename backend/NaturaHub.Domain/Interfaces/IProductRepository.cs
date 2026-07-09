using NaturaHub.Domain.Entities;

namespace NaturaHub.Domain.Interfaces;

public interface IProductRepository
{
    // Busca um produto pelo Id — retorna null se não encontrar
    // O "?" no Product? significa que pode retornar nulo
    Task<Product?> GetByIdAsync(int id);

    // Lista todos os produtos
    // activeOnly: true = só produtos ativos | false = todos (incluindo arquivados)
    Task<IEnumerable<Product>> GetAllAsync(bool activeOnly = true);

    // Cria um produto novo no banco
    Task AddAsync(Product product);

    // Atualiza um produto existente
    Task UpdateAsync(Product product);

    // Verifica se um produto existe — útil para validações antes de registrar movimentações
    Task<bool> ExistsAsync(int id);
}
