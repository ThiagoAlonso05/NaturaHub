using NaturaHub.Application.DTOs.Requests;
using NaturaHub.Application.DTOs.Responses;
using NaturaHub.Application.Interfaces;
using NaturaHub.Domain.Entities;
using NaturaHub.Domain.Interfaces;
using NaturaHub.Domain.Enums;

namespace NaturaHub.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IStockRepository _stockRepository;

    // Injeção de múltiplos repositórios: 
    // O ProductService orquestra a criação do Produto E do seu Estoque inicial
    public ProductService(
        IProductRepository productRepository, 
        ICategoryRepository categoryRepository,
        IStockRepository stockRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _stockRepository = stockRepository;
    }

    public async Task<ProductResponse?> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return null;

        return MapToResponse(product);
    }

    public async Task<IEnumerable<ProductResponse>> GetAllAsync(bool activeOnly = true)
    {
        var products = await _productRepository.GetAllAsync(activeOnly);
        return products.Select(MapToResponse);
    }

    public async Task<ProductResponse> CreateAsync(CreateProductRequest request)
    {
        // 1. Validação de Negócio: A categoria existe?
        var categoryExists = await _categoryRepository.ExistsAsync(request.CategoryId);
        if (!categoryExists)
            throw new ArgumentException("Categoria informada não existe.");

        // 2. Cria a entidade do Produto
        var product = new Product(
            request.Name,
            request.CategoryId,
            request.CatalogPrice,
            request.NaturaCode,
            request.SKU,
            request.ImageUrl,
            request.Description);

        // 3. Salva o produto (ele vai gerar o Id no banco)
        await _productRepository.AddAsync(product);

        // 4. Cria a entidade de Estoque atrelada a esse Produto (relacionamento 1:1)
        var stock = new Stock(product.Id, request.InitialQuantity);
        
        // 5. Salva o Estoque
        await _stockRepository.AddAsync(stock);

        // Recarrega o produto para preencher os dados de navegação (Category, etc) antes de devolver
        var savedProduct = await _productRepository.GetByIdAsync(product.Id);

        return MapToResponse(savedProduct!);
    }

    public async Task<ProductResponse?> UpdateAsync(int id, UpdateProductRequest request)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return null;

        // Validação: Se trocou a categoria, a nova existe?
        if (product.CategoryId != request.CategoryId)
        {
            var categoryExists = await _categoryRepository.ExistsAsync(request.CategoryId);
            if (!categoryExists)
                throw new ArgumentException("Categoria informada não existe.");
        }

        product.Update(
            request.Name,
            request.CategoryId,
            request.CatalogPrice,
            request.NaturaCode,
            request.SKU,
            request.ImageUrl,
            request.Description);

        await _productRepository.UpdateAsync(product);

        // Recarrega para garantir que os dados da categoria preenchidos na Resposta estejam atualizados
        var updatedProduct = await _productRepository.GetByIdAsync(id);

        return MapToResponse(updatedProduct!);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null) return false;

        // Soft Delete: chamamos o Deactivate() igual fizemos na categoria
        product.Deactivate();

        // O UpdateAsync do repositório vai salvar a alteração do StatusId
        await _productRepository.UpdateAsync(product);

        return true;
    }

    private static ProductResponse MapToResponse(Product product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            NaturaCode = product.NaturaCode,
            SKU = product.SKU,
            CategoryId = product.CategoryId,
            // Como usamos Eager Loading no repositório (Include), Category não é nulo.
            CategoryName = product.Category?.Name ?? string.Empty,
            ImageUrl = product.ImageUrl,
            CatalogPrice = product.CatalogPrice,
            Description = product.Description,
            Status = product.StatusId == (int)DefaultStatus.Active ? "Ativo" : "Inativo",
            // Se o estoque não foi carregado ainda por algum motivo, retorna 0.
            CurrentStockQuantity = product.Stock?.CurrentQuantity ?? 0,
            UpdatedAt = product.UpdatedAt
        };
    }
}
