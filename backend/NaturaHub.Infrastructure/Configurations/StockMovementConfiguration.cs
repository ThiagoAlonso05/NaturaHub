using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NaturaHub.Domain.Entities;
using NaturaHub.Domain.Enums;

namespace NaturaHub.Infrastructure.Configurations;

public class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
{
    public void Configure(EntityTypeBuilder<StockMovement> builder)
    {
        builder.ToTable("StockMovements");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Quantity)
            .IsRequired();

        builder.Property(m => m.UnitPrice)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(m => m.Notes)
            .HasMaxLength(500);

        builder.Property(m => m.MovementDate)
            .IsRequired();

        // Converte o enum MovementType para string no banco
        // Em vez de salvar 1 ou 2, salva "In" ou "Out"
        // Muito mais legível se alguém consultar o banco diretamente
        builder.Property(m => m.MovementType)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(10);

        // Índice para busca por produto — nossa query mais comum
        builder.HasIndex(m => m.ProductId);
    }
}
