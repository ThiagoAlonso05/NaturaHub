using NaturaHub.Application.DTOs.Requests;
using NaturaHub.Application.DTOs.Responses;

namespace NaturaHub.Application.Interfaces;

public interface IStockMovementService
{
    Task<IEnumerable<StockMovementResponse>> GetHistoryByProductIdAsync(int productId);
    
    // O retorno pode ser string se falhar em regras de negócio (ex: "Estoque insuficiente")
    // Em APIs mais complexas usaríamos o pattern Result<T>.
    // Por simplicidade, vamos usar Exceptions para erros de negócio e só retornar o DTO
    Task<StockMovementResponse> CreateAsync(CreateStockMovementRequest request);
    
    // Para edição, lidamos com os recálculos complexos de estoque
    Task<StockMovementResponse?> UpdateAsync(int id, UpdateStockMovementRequest request);
}
