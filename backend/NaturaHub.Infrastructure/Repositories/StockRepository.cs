using Microsoft.EntityFrameworkCore;
using NaturaHub.Domain.Entities;
using NaturaHub.Domain.Interfaces;
using NaturaHub.Infrastructure.Data;

namespace NaturaHub.Infrastructure.Repositories;

public class StockRepository : IStockRepository
{
    private readonly NaturaHubDbContext _context;

    public StockRepository(NaturaHubDbContext context)
    {
        _context = context;
    }

    public async Task<Stock?> GetByProductIdAsync(int productId)
    {
        // Aqui NÃO usamos AsNoTracking — o Stock será modificado pelo Service
        // (AddQuantity ou RemoveQuantity) e depois salvo com UpdateAsync
        // O Change Tracker precisa estar ativo para rastrear as mudanças
        return await _context.Stocks
            .FirstOrDefaultAsync(s => s.ProductId == productId);
    }

    public async Task AddAsync(Stock stock)
    {
        await _context.Stocks.AddAsync(stock);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Stock stock)
    {
        _context.Stocks.Update(stock);
        await _context.SaveChangesAsync();
    }
}
