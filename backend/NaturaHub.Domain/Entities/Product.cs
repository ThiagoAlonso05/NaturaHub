using NaturaHub.Domain.Common;

namespace NaturaHub.Domain.Entities;

public class Product : BaseStatusEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? NaturaCode { get; private set; }
    public string? SKU { get; private set; }
    public int CategoryId { get; private set; }
    public string? ImageUrl { get; private set; }
    public decimal CatalogPrice { get; private set; }
    public string? Description { get; private set; }
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

    // Navegações
    public Category? Category { get; private set; }
    public Stock? Stock { get; private set; }
    public ICollection<StockMovement> StockMovements { get; private set; } = [];

    private Product() { }

    public Product(
        string name,
        int categoryId,
        decimal catalogPrice,
        string? naturaCode = null,
        string? sku = null,
        string? imageUrl = null,
        string? description = null)
    {
        Name = name;
        CategoryId = categoryId;
        CatalogPrice = catalogPrice;
        NaturaCode = naturaCode;
        SKU = sku;
        ImageUrl = imageUrl;
        Description = description;
        // StatusId = Active já vem por padrão do BaseStatusEntity
    }

    public void Update(
        string name,
        int categoryId,
        decimal catalogPrice,
        string? naturaCode,
        string? sku,
        string? imageUrl,
        string? description)
    {
        Name = name;
        CategoryId = categoryId;
        CatalogPrice = catalogPrice;
        NaturaCode = naturaCode;
        SKU = sku;
        ImageUrl = imageUrl;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }
}
