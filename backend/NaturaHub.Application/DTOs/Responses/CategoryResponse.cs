namespace NaturaHub.Application.DTOs.Responses;

public class CategoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    // Retorna o nome do status em vez do Id numérico
    // O frontend exibe "Ativo" ou "Inativo", não "1" ou "2"
    public string Status { get; set; } = string.Empty;
}
