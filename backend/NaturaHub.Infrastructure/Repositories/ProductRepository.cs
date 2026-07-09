using Microsoft.EntityFrameworkCore;
using NaturaHub.Domain.Entities;
using NaturaHub.Domain.Enums;
using NaturaHub.Domain.Interfaces;
using NaturaHub.Infrastructure.Data;

namespace NaturaHub.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly NaturaHubDbContext _context;

    public ProductRepository(NaturaHubDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        // Include: carrega as navegações junto com o produto (eager loading)
        // Sem Include, Category e Stock viriam como null mesmo existindo no banco
        return await _context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.Stock)
            .Include(p => p.Status)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync(bool activeOnly = true)
    {
        var query = _context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.Stock)
            .Include(p => p.Status)
            .AsQueryable();

        if (activeOnly)
            query = query.Where(p => p.StatusId == (int)DefaultStatus.Active);

        return await query
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Products
            .AnyAsync(p => p.Id == id);
    }
}
