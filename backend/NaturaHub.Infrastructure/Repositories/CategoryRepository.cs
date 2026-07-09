using Microsoft.EntityFrameworkCore;
using NaturaHub.Domain.Entities;
using NaturaHub.Domain.Enums;
using NaturaHub.Domain.Interfaces;
using NaturaHub.Infrastructure.Data;

namespace NaturaHub.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly NaturaHubDbContext _context;

    // O DbContext é injetado pelo container de DI — não instanciamos manualmente
    public CategoryRepository(NaturaHubDbContext context)
    {
        _context = context;
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        // AsNoTracking: diz ao EF Core para não rastrear essa entidade
        // Usamos quando só vamos ler, sem modificar — melhora a performance
        return await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Category>> GetAllAsync(bool activeOnly = true)
    {
        var query = _context.Categories.AsNoTracking();

        if (activeOnly)
            query = query.Where(c => c.StatusId == (int)DefaultStatus.Active);

        return await query
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task AddAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        // Update marca a entidade como modificada no Change Tracker
        // SaveChangesAsync gera o UPDATE no banco apenas com os campos alterados
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Category category)
    {
        // A entidade já teve Deactivate() chamado no Service antes de chegar aqui
        // O Repository só persiste o estado atual da entidade
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        // AnyAsync é mais eficiente que buscar a entidade inteira só para checar existência
        // Gera: SELECT CASE WHEN EXISTS(...) THEN 1 ELSE 0 END
        return await _context.Categories
            .AnyAsync(c => c.Id == id);
    }
}
