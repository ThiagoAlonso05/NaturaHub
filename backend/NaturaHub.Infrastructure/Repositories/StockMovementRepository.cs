using Microsoft.EntityFrameworkCore;
using NaturaHub.Domain.Entities;
using NaturaHub.Domain.Enums;
using NaturaHub.Domain.Interfaces;
using NaturaHub.Infrastructure.Data;

namespace NaturaHub.Infrastructure.Repositories;

public class StockMovementRepository : IStockMovementRepository
{
    private readonly NaturaHubDbContext _context;

    public StockMovementRepository(NaturaHubDbContext context)
    {
        _context = context;
    }

    public async Task<StockMovement?> GetByIdAsync(int id)
    {
        // Sem AsNoTracking — pode ser carregado para edição
        return await _context.StockMovements
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<IEnumerable<StockMovement>> GetByProductIdAsync(int productId)
    {
        return await _context.StockMovements
            .AsNoTracking()
            .Where(m => m.ProductId == productId)
            .OrderByDescending(m => m.MovementDate) // mais recente primeiro
            .ToListAsync();
    }

    public async Task<IEnumerable<StockMovement>> GetByProductIdAndTypeAsync(
        int productId,
        MovementType type)
    {
        return await _context.StockMovements
            .AsNoTracking()
            .Where(m => m.ProductId == productId && m.MovementType == type)
            .OrderByDescending(m => m.MovementDate)
            .ToListAsync();
    }

    public async Task AddAsync(StockMovement movement)
    {
        await _context.StockMovements.AddAsync(movement);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(StockMovement movement)
    {
        _context.StockMovements.Update(movement);
        await _context.SaveChangesAsync();
    }
}
