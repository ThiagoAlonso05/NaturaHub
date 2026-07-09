using NaturaHub.Domain.Common;
using NaturaHub.Domain.Enums;

namespace NaturaHub.Domain.Entities;

public class StockMovement : BaseEntity
{
    public int ProductId { get; private set; }
    public MovementType MovementType { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }  // preço pago (IN) ou vendido (OUT)
    public string? Notes { get; private set; }
    public DateTime MovementDate { get; private set; }

    // Valor total da movimentação — calculado em memória
    public decimal TotalValue => Quantity * UnitPrice;

    public Product? Product { get; private set; }

    private StockMovement() { }

    public StockMovement(
        int productId,
        MovementType movementType,
        int quantity,
        decimal unitPrice,
        DateTime movementDate,
        string? notes = null)
    {
        ProductId = productId;
        MovementType = movementType;
        Quantity = quantity;
        UnitPrice = unitPrice;
        MovementDate = movementDate;
        Notes = notes;
    }

    // Método adicionado para permitir edição de erros de digitação
    public void Update(int quantity, decimal unitPrice, DateTime movementDate, string? notes)
    {
        Quantity = quantity;
        UnitPrice = unitPrice;
        MovementDate = movementDate;
        Notes = notes;
    }
}
