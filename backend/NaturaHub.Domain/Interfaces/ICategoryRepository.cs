using NaturaHub.Domain.Entities;

namespace NaturaHub.Domain.Interfaces;

public interface ICategoryRepository
{
    // Busca uma categoria pelo Id
    Task<Category?> GetByIdAsync(int id);

    // Lista categorias
    // activeOnly: true = só ativas | false = todas (incluindo inativas)
    Task<IEnumerable<Category>> GetAllAsync(bool activeOnly = true);

    // Cria uma nova categoria
    Task AddAsync(Category category);

    // Atualiza nome ou descrição
    Task UpdateAsync(Category category);

    // Soft delete — chama Deactivate() na entidade e persiste
    // Produtos vinculados continuam existindo normalmente
    Task DeleteAsync(Category category);

    // Verifica existência — usada antes de salvar produto com aquele CategoryId
    Task<bool> ExistsAsync(int id);
}
