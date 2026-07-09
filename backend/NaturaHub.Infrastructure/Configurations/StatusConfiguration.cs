using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NaturaHub.Domain.Entities;

namespace NaturaHub.Infrastructure.Configurations;

public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        builder.ToTable("Statuses");

        builder.HasKey(s => s.Id);

        // Id manual — não é auto-incremento porque os valores são fixos (seed)
        builder.Property(s => s.Id)
            .ValueGeneratedNever();

        builder.Property(s => s.Key)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.Description)
            .HasMaxLength(300);

        // Índice único no Key — não pode ter dois status com a mesma chave textual
        builder.HasIndex(s => s.Key).IsUnique();

        // Seed — dados iniciais que sempre existirão no banco
        builder.HasData(
            new { Id = 1, Key = "ACTIVE",   Name = "Ativo",   Description = "Registro ativo no sistema" },
            new { Id = 2, Key = "INACTIVE", Name = "Inativo", Description = "Registro inativo no sistema" }
        );
    }
}
