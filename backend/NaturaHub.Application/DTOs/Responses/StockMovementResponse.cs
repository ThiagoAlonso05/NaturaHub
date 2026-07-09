namespace NaturaHub.Application.DTOs.Responses;

public class StockMovementResponse
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    
    // Nome do produto para facilitar exibição
    public string ProductName { get; set; } = string.Empty;
    
    // Opcionalmente podemos retornar como string para facilitar pro frontend
    public string MovementType { get; set; } = string.Empty;
    
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalValue { get; set; }
    public string? Notes { get; set; }
    public DateTime MovementDate { get; set; }
}
