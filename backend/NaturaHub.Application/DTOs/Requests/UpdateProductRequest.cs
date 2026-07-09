namespace NaturaHub.Application.DTOs.Requests;

public class UpdateProductRequest
{
    public string Name { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public decimal CatalogPrice { get; set; }
    public string? NaturaCode { get; set; }
    public string? SKU { get; set; }
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
}
