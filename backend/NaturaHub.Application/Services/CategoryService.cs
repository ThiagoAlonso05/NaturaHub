using NaturaHub.Application.DTOs.Requests;
using NaturaHub.Application.DTOs.Responses;
using NaturaHub.Application.Interfaces;
using NaturaHub.Domain.Entities;
using NaturaHub.Domain.Interfaces;
using NaturaHub.Domain.Enums;

namespace NaturaHub.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    // Injeção de dependência do repositório
    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryResponse?> GetByIdAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        
        if (category == null) return null;

        return MapToResponse(category);
    }

    public async Task<IEnumerable<CategoryResponse>> GetAllAsync(bool activeOnly = true)
    {
        var categories = await _categoryRepository.GetAllAsync(activeOnly);
        return categories.Select(MapToResponse);
    }

    public async Task<CategoryResponse> CreateAsync(CreateCategoryRequest request)
    {
        // 1. O Serviço recebe o DTO de Request
        // 2. Transforma o DTO na Entidade de Domínio
        var category = new Category(request.Name, request.Description);

        // 3. Manda o Repositório salvar a Entidade
        await _categoryRepository.AddAsync(category);

        // 4. Transforma a Entidade salva em um DTO de Response e devolve
        return MapToResponse(category);
    }

    public async Task<CategoryResponse?> UpdateAsync(int id, UpdateCategoryRequest request)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        
        if (category == null) return null;

        // Regra de negócio: chamamos o método Update da entidade
        category.Update(request.Name, request.Description);

        await _categoryRepository.UpdateAsync(category);

        return MapToResponse(category);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _categoryRepository.GetByIdAsync(id);
        
        if (category == null) return false;

        // Regra de negócio: Soft Delete!
        // Chamamos o Deactivate (que muda o status para Inactive).
        category.Deactivate();

        // O repositório vai salvar essa alteração no banco
        await _categoryRepository.DeleteAsync(category);

        return true;
    }

    // Método auxiliar (Mapeador manual). 
    // Em projetos maiores, costuma-se usar bibliotecas como AutoMapper para não fazer isso na mão.
    // Mas para o MVP, mapeamento manual é excelente pois é fácil de debugar.
    private static CategoryResponse MapToResponse(Category category)
    {
        return new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            // Convertemos o Id do status numérico para uma string mais legível
            Status = category.StatusId == (int)DefaultStatus.Active ? "Ativo" : "Inativo"
        };
    }
}
