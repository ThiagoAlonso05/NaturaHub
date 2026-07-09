namespace NaturaHub.Domain.Entities;

// Status é uma tabela de lookup — não herda BaseEntity
// porque não faz sentido um status ter CreatedAt ou ser rastreado como entidade de negócio
// É uma tabela de referência, como uma constante no banco
public class Status
{
    public int Id { get; private set; }

    // Identificador textual único — facilita uso no código sem depender do Id numérico
    // Exemplos: "ACTIVE", "INACTIVE", "PENDING", "DELIVERED"
    public string Key { get; private set; } = string.Empty;

    // Nome amigável exibido na interface
    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    // Navegações
    public ICollection<Category> Categories { get; private set; } = [];
    public ICollection<Product> Products { get; private set; } = [];

    private Status() { }

    public Status(int id, string key, string name, string? description = null)
    {
        Id = id;
        Key = key;
        Name = name;
        Description = description;
    }
}
