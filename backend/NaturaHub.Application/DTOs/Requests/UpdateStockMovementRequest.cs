using NaturaHub.Domain.Enums;

namespace NaturaHub.Application.DTOs.Requests;

public class UpdateStockMovementRequest
{
    // Apenas os dados mutáveis (erros de digitação comuns)
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string? Notes { get; set; }
    public DateTime MovementDate { get; set; }
}
