using NaturaHub.Domain.Entities;
using NaturaHub.Domain.Enums;

namespace NaturaHub.Domain.Interfaces;

public interface IStockMovementRepository
{
    // Busca uma movimentação pelo Id — usada para carregar antes de editar
    Task<StockMovement?> GetByIdAsync(int id);

    // Busca todo o histórico de movimentações de um produto
    Task<IEnumerable<StockMovement>> GetByProductIdAsync(int productId);

    // Busca movimentações filtrando por tipo (In ou Out)
    // Usada no Sprint 3 para calcular lucro e custo médio
    Task<IEnumerable<StockMovement>> GetByProductIdAndTypeAsync(int productId, MovementType type);

    // Registra uma nova movimentação
    Task AddAsync(StockMovement movement);

    // Atualiza uma movimentação existente
    // Cenário: usuário errou a quantidade ou o preço e quer corrigir
    // O frontend pedirá confirmação antes de chamar esse endpoint
    Task UpdateAsync(StockMovement movement);
}
