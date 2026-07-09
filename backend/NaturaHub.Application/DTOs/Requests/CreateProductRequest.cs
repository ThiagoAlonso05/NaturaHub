namespace NaturaHub.Application.DTOs.Requests;

public class CreateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public decimal CatalogPrice { get; set; }
    public string? NaturaCode { get; set; }
    public string? SKU { get; set; }
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }

    // Quantidade inicial informada no momento do cadastro
    // Vai criar o registro de Stock com esse valor
    public int InitialQuantity { get; set; } = 0;
}
