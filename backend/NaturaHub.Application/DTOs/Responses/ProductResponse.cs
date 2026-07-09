namespace NaturaHub.Application.DTOs.Responses;

public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? NaturaCode { get; set; }
    public string? SKU { get; set; }
    
    // Devolvemos o ID e o Nome da Categoria juntos.
    // Assim o frontend não precisa fazer outra requisição para saber o nome da categoria.
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    
    public string? ImageUrl { get; set; }
    public decimal CatalogPrice { get; set; }
    public string? Description { get; set; }
    
    // Status amigável para exibição
    public string Status { get; set; } = string.Empty;

    // Quantidade em estoque "achatada" na resposta
    public int CurrentStockQuantity { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}
