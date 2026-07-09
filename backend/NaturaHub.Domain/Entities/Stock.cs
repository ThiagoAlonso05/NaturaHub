using NaturaHub.Domain.Common;

namespace NaturaHub.Domain.Entities;

public class Stock : BaseEntity
{
    public int ProductId { get; private set; }
    public int CurrentQuantity { get; private set; }
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

    // Navegação
    public Product? Product { get; private set; }

    private Stock() { }

    public Stock(int productId, int initialQuantity)
    {
        ProductId = productId;
        CurrentQuantity = initialQuantity;
    }

    public void AddQuantity(int quantity)
    {
        if (quantity <= 0) throw new ArgumentException("Quantidade deve ser maior que zero.");
        CurrentQuantity += quantity;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveQuantity(int quantity)
    {
        if (quantity <= 0) throw new ArgumentException("Quantidade deve ser maior que zero.");
        if (quantity > CurrentQuantity) throw new InvalidOperationException("Estoque insuficiente.");
        CurrentQuantity -= quantity;
        UpdatedAt = DateTime.UtcNow;
    }
}
