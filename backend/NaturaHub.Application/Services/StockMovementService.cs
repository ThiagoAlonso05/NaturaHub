using NaturaHub.Application.DTOs.Requests;
using NaturaHub.Application.DTOs.Responses;
using NaturaHub.Application.Interfaces;
using NaturaHub.Domain.Entities;
using NaturaHub.Domain.Enums;
using NaturaHub.Domain.Interfaces;

namespace NaturaHub.Application.Services;

public class StockMovementService : IStockMovementService
{
    private readonly IStockMovementRepository _movementRepository;
    private readonly IStockRepository _stockRepository;
    private readonly IProductRepository _productRepository;

    public StockMovementService(
        IStockMovementRepository movementRepository,
        IStockRepository stockRepository,
        IProductRepository productRepository)
    {
        _movementRepository = movementRepository;
        _stockRepository = stockRepository;
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<StockMovementResponse>> GetHistoryByProductIdAsync(int productId)
    {
        var movements = await _movementRepository.GetByProductIdAsync(productId);
        
        // Em um sistema maior, usaríamos um JOIN na query do repository.
        // Como aqui as categorias/produtos são pequenas, faremos a leitura manual
        var product = await _productRepository.GetByIdAsync(productId);
        var productName = product?.Name ?? "Produto Desconhecido";

        return movements.Select(m => MapToResponse(m, productName));
    }

    public async Task<StockMovementResponse> CreateAsync(CreateStockMovementRequest request)
    {
        // 1. Verifica se o produto existe
        var productExists = await _productRepository.ExistsAsync(request.ProductId);
        if (!productExists)
            throw new ArgumentException("Produto não encontrado.");

        // 2. Busca o estoque atual daquele produto
        var stock = await _stockRepository.GetByProductIdAsync(request.ProductId);
        if (stock == null)
            throw new InvalidOperationException("Estoque não inicializado para este produto.");

        // 3. Aplica as regras de negócio de Estoque
        if (request.MovementType == MovementType.In)
        {
            stock.AddQuantity(request.Quantity);
        }
        else if (request.MovementType == MovementType.Out)
        {
            // O próprio método RemoveQuantity já lança erro se não tiver saldo suficiente
            stock.RemoveQuantity(request.Quantity);
        }

        // 4. Cria a movimentação
        var movement = new StockMovement(
            request.ProductId,
            request.MovementType,
            request.Quantity,
            request.UnitPrice,
            request.MovementDate ?? DateTime.UtcNow,
            request.Notes);

        // 5. Salva tudo (ambos Repositórios). 
        // No EF Core, se os dois usam o mesmo DbContext injetado, 
        // um SaveChangesAsync de qualquer um deles salva tudo de uma vez numa única transação.
        await _stockRepository.UpdateAsync(stock);
        await _movementRepository.AddAsync(movement);

        var product = await _productRepository.GetByIdAsync(request.ProductId);
        return MapToResponse(movement, product?.Name ?? string.Empty);
    }

    public async Task<StockMovementResponse?> UpdateAsync(int id, UpdateStockMovementRequest request)
    {
        var movement = await _movementRepository.GetByIdAsync(id);
        if (movement == null) return null;

        var stock = await _stockRepository.GetByProductIdAsync(movement.ProductId);
        if (stock == null)
            throw new InvalidOperationException("Estoque não encontrado para recalcular.");

        // LÓGICA DE RECALCULO DE ESTOQUE:
        // Primeiro, desfazemos a operação original do estoque
        if (movement.MovementType == MovementType.In)
            stock.RemoveQuantity(movement.Quantity); // Se foi uma entrada de 10, removemos 10 do estoque
        else
            stock.AddQuantity(movement.Quantity);    // Se foi saída de 5, devolvemos 5 pro estoque

        // Agora, aplicamos a nova quantidade que o usuário informou na edição
        if (movement.MovementType == MovementType.In)
            stock.AddQuantity(request.Quantity);
        else
            stock.RemoveQuantity(request.Quantity);

        // Atualizamos a entidade da movimentação em si
        movement.Update(request.Quantity, request.UnitPrice, request.MovementDate, request.Notes);

        await _movementRepository.UpdateAsync(movement);
        await _stockRepository.UpdateAsync(stock);

        var product = await _productRepository.GetByIdAsync(movement.ProductId);
        return MapToResponse(movement, product?.Name ?? string.Empty);
    }

    private static StockMovementResponse MapToResponse(StockMovement movement, string productName)
    {
        return new StockMovementResponse
        {
            Id = movement.Id,
            ProductId = movement.ProductId,
            ProductName = productName,
            MovementType = movement.MovementType == MovementType.In ? "Entrada" : "Saída",
            Quantity = movement.Quantity,
            UnitPrice = movement.UnitPrice,
            TotalValue = movement.TotalValue,
            Notes = movement.Notes,
            MovementDate = movement.MovementDate
        };
    }
}
