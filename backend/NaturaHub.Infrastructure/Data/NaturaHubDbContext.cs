using Microsoft.EntityFrameworkCore;
using NaturaHub.Domain.Entities;
using NaturaHub.Infrastructure.Configurations;

namespace NaturaHub.Infrastructure.Data;

// DbContext é a sessão com o banco de dados
// É através dele que fazemos queries, inserções, atualizações e deleções
// Ele também mantém o rastreamento de mudanças nas entidades (Change Tracker)
public class NaturaHubDbContext : DbContext
{
    public NaturaHubDbContext(DbContextOptions<NaturaHubDbContext> options) : base(options) { }

    // DbSet representa uma tabela no banco
    // É através deles que escrevemos queries: _context.Products.Where(...)
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<StockMovement> StockMovements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplica todas as configurações automaticamente
        // Busca todas as classes que implementam IEntityTypeConfiguration<> neste assembly
        // Assim não precisamos chamar cada uma manualmente
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NaturaHubDbContext).Assembly);
    }
}
