using NaturaHub.Domain.Entities;

namespace NaturaHub.Domain.Interfaces;

public interface IStockRepository
{
    // Busca o estoque de um produto específico
    // Usada toda vez que precisamos saber a quantidade atual antes de uma operação
    Task<Stock?> GetByProductIdAsync(int productId);

    // Cria o registro inicial de estoque quando um produto é cadastrado
    Task AddAsync(Stock stock);

    // Atualiza o estoque após uma entrada ou saída
    // Chamada depois de AddQuantity() ou RemoveQuantity() na entidade
    Task UpdateAsync(Stock stock);
}
