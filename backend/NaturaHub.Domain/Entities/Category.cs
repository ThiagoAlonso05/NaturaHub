using NaturaHub.Domain.Common;

namespace NaturaHub.Domain.Entities;

public class Category : BaseStatusEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }

    // Navegação
    public ICollection<Product> Products { get; private set; } = [];

    private Category() { }

    public Category(string name, string? description = null)
    {
        Name = name;
        Description = description;
        // StatusId = Active já vem por padrão do BaseStatusEntity
    }

    public void Update(string name, string? description)
    {
        Name = name;
        Description = description;
    }
}
