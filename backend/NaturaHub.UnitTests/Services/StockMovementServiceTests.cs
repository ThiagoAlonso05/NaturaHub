using FluentAssertions;
using Moq;
using NaturaHub.Application.DTOs.Requests;
using NaturaHub.Application.Services;
using NaturaHub.Domain.Entities;
using NaturaHub.Domain.Enums;
using NaturaHub.Domain.Interfaces;

namespace NaturaHub.UnitTests.Services;

public class StockMovementServiceTests
{
    private readonly Mock<IStockMovementRepository> _movementRepoMock;
    private readonly Mock<IStockRepository> _stockRepoMock;
    private readonly Mock<IProductRepository> _productRepoMock;
    private readonly StockMovementService _service;

    public StockMovementServiceTests()
    {
        _movementRepoMock = new Mock<IStockMovementRepository>();
        _stockRepoMock = new Mock<IStockRepository>();
        _productRepoMock = new Mock<IProductRepository>();
        
        // Injetamos os Mocks (Falsos) no Serviço Verdadeiro
        _service = new StockMovementService(_movementRepoMock.Object, _stockRepoMock.Object, _productRepoMock.Object);
    }

    [Fact]
    public async Task CreateAsync_ShouldAddStock_WhenMovementIsIn()
    {
        // ==========================================
        // ARRANGE
        // ==========================================
        var request = new CreateStockMovementRequest
        {
            ProductId = 1,
            MovementType = MovementType.In,
            Quantity = 10,
            UnitPrice = 50.0m,
            Notes = "Compra de fornecedor",
            MovementDate = DateTime.UtcNow
        };

        var fakeStock = new Stock(1, 5);

        _productRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Product("Produto", 1, 10m));
        _productRepoMock.Setup(r => r.ExistsAsync(1)).ReturnsAsync(true);
        _stockRepoMock.Setup(r => r.GetByProductIdAsync(1)).ReturnsAsync(fakeStock);

        // ==========================================
        // ACT
        // ==========================================
        var result = await _service.CreateAsync(request);

        // ==========================================
        // ASSERT
        // ==========================================
        fakeStock.CurrentQuantity.Should().Be(15);
        _movementRepoMock.Verify(r => r.AddAsync(It.IsAny<StockMovement>()), Times.Once);
        _stockRepoMock.Verify(r => r.UpdateAsync(fakeStock), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ShouldRecalculateStock_WhenChangingQuantity()
    {
        // ==========================================
        // ARRANGE
        // ==========================================
        var originalMovement = new StockMovement(1, MovementType.In, 10, 50.0m, DateTime.UtcNow, "Original");
        
        var request = new UpdateStockMovementRequest
        {
            Quantity = 15,
            UnitPrice = 50.0m,
            Notes = "Corrigido",
            MovementDate = DateTime.UtcNow
        };

        var fakeStock = new Stock(1, 10);

        _productRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Product("Produto", 1, 10m));
        _movementRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(originalMovement);
        _stockRepoMock.Setup(r => r.GetByProductIdAsync(1)).ReturnsAsync(fakeStock);

        // ==========================================
        // ACT
        // ==========================================
        await _service.UpdateAsync(1, request);

        // ==========================================
        // ASSERT
        // ==========================================
        fakeStock.CurrentQuantity.Should().Be(15);
        originalMovement.Quantity.Should().Be(15);
    }
}
