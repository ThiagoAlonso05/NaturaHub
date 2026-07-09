using NaturaHub.Application.DTOs.Requests;
using NaturaHub.Application.DTOs.Responses;

namespace NaturaHub.Application.Interfaces;

public interface IProductService
{
    Task<ProductResponse?> GetByIdAsync(int id);
    Task<IEnumerable<ProductResponse>> GetAllAsync(bool activeOnly = true);
    Task<ProductResponse> CreateAsync(CreateProductRequest request);
    Task<ProductResponse?> UpdateAsync(int id, UpdateProductRequest request);
    Task<bool> DeleteAsync(int id);
}
