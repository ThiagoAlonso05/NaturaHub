using NaturaHub.Domain.Entities;
using NaturaHub.Domain.Enums;

namespace NaturaHub.Domain.Common;

// Classe base para entidades que possuem controle de status
// Herda de BaseEntity (Id + CreatedAt) e adiciona StatusId
// Use quando a entidade precisa ser ativada/desativada no sistema
public abstract class BaseStatusEntity : BaseEntity
{
    public int StatusId { get; protected set; } = (int)DefaultStatus.Active;

    // Navegação — EF Core usa para montar o JOIN com a tabela Status
    public Status? Status { get; private set; }

    public void Activate() => StatusId = (int)DefaultStatus.Active;
    public void Deactivate() => StatusId = (int)DefaultStatus.Inactive;
}
