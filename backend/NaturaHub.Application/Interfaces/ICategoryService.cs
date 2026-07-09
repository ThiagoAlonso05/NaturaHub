using NaturaHub.Application.DTOs.Requests;
using NaturaHub.Application.DTOs.Responses;

namespace NaturaHub.Application.Interfaces;

public interface ICategoryService
{
    Task<CategoryResponse?> GetByIdAsync(int id);
    Task<IEnumerable<CategoryResponse>> GetAllAsync(bool activeOnly = true);
    
    // Repare que recebemos o Request (DTO) e devolvemos o Response (DTO)
    // O chamador (a Controller da API) não faz ideia de que a entidade "Category" existe.
    Task<CategoryResponse> CreateAsync(CreateCategoryRequest request);
    Task<CategoryResponse?> UpdateAsync(int id, UpdateCategoryRequest request);
    
    // Retorna um bool indicando se deu certo ou se falhou (ex: não encontrou o id)
    Task<bool> DeleteAsync(int id);
}
