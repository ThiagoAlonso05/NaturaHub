using FluentAssertions;
using Moq;
using NaturaHub.Application.DTOs;
using NaturaHub.Application.Services;
using NaturaHub.Domain.Entities;
using NaturaHub.Domain.Interfaces;

namespace NaturaHub.UnitTests.Services;

public class CategoryServiceTests
{
    // O mock do repositório (Nosso "Banco de Dados Falso")
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
    
    // O serviço real que estamos testando
    private readonly CategoryService _categoryService;

    public CategoryServiceTests()
    {
        // 1. Instanciamos o Mock (Falso)
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        
        // 2. Injetamos o falso dentro do serviço verdadeiro
        _categoryService = new CategoryService(_categoryRepositoryMock.Object);
    }

    [Fact] // Indica pro xUnit que isso é um teste que deve ser rodado
    public async Task GetByIdAsync_ShouldReturnCategory_WhenItExists()
    {
        // ==========================================
        // ARRANGE (Preparar)
        // ==========================================
        var categoryId = 1;
        var fakeCategory = new Category("Perfumes", "Linha masculina");
        
        // Ensinando o Mock a responder: "Quando alguém chamar o GetByIdAsync(1), retorne a fakeCategory"
        _categoryRepositoryMock
            .Setup(repo => repo.GetByIdAsync(categoryId))
            .ReturnsAsync(fakeCategory);

        // ==========================================
        // ACT (Agir)
        // ==========================================
        // Chamamos o método de verdade. Ele internamente vai chamar o Mock.
        var result = await _categoryService.GetByIdAsync(categoryId);

        // ==========================================
        // ASSERT (Verificar)
        // ==========================================
        // Verificamos usando o FluentAssertions se a resposta faz sentido
        result.Should().NotBeNull();
        result!.Name.Should().Be("Perfumes");
    }
}
