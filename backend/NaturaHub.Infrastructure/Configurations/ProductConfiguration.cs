using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NaturaHub.Domain.Entities;

namespace NaturaHub.Infrastructure.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.NaturaCode)
            .HasMaxLength(50);

        builder.Property(p => p.SKU)
            .HasMaxLength(50);

        builder.Property(p => p.ImageUrl)
            .HasMaxLength(500);

        builder.Property(p => p.CatalogPrice)
            .IsRequired()
            .HasPrecision(10, 2); // até 99.999.999,99

        builder.Property(p => p.Description)
            .HasMaxLength(1000);

        builder.Property(p => p.StatusId)
            .IsRequired()
            .HasDefaultValue(1); // Active

        // Relacionamento: Product → Category (muitos para um)
        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento: Product → Status (muitos para um)
        builder.HasOne(p => p.Status)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento: Product → Stock (um para um)
        // Um produto tem exatamente um registro de estoque
        builder.HasOne(p => p.Stock)
            .WithOne(s => s.Product)
            .HasForeignKey<Stock>(s => s.ProductId)
            .OnDelete(DeleteBehavior.Cascade); // se deletar produto, deleta o estoque junto

        // Relacionamento: Product → StockMovements (um para muitos)
        builder.HasMany(p => p.StockMovements)
            .WithOne(m => m.Product)
            .HasForeignKey(m => m.ProductId)
            .OnDelete(DeleteBehavior.Cascade); // se deletar produto, deleta o histórico junto
    }
}
