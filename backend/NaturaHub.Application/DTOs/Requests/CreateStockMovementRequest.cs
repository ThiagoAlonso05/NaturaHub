using NaturaHub.Domain.Enums;

namespace NaturaHub.Application.DTOs.Requests;

public class CreateStockMovementRequest
{
    public int ProductId { get; set; }
    
    // O frontend pode mandar a string "In" ou "Out", ou o valor numérico (1 ou 2)
    // Na API vamos mapear isso para o Enum
    public MovementType MovementType { get; set; }
    
    public int Quantity { get; set; }
    
    public decimal UnitPrice { get; set; }
    
    public string? Notes { get; set; }
    
    // A data pode ser enviada pelo usuário caso seja um lançamento retroativo
    public DateTime? MovementDate { get; set; }
}
