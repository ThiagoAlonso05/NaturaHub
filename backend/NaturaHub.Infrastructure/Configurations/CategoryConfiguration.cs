using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NaturaHub.Domain.Entities;

namespace NaturaHub.Infrastructure.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.Description)
            .HasMaxLength(300);

        builder.Property(c => c.StatusId)
            .IsRequired()
            .HasDefaultValue(1); // Active

        // Relacionamento: Category → Status (muitos para um)
        // Uma categoria tem um status. Um status pode estar em muitas categorias.
        builder.HasOne(c => c.Status)
            .WithMany(s => s.Categories)
            .HasForeignKey(c => c.StatusId)
            .OnDelete(DeleteBehavior.Restrict); // não permite deletar um status que está em uso
    }
}
