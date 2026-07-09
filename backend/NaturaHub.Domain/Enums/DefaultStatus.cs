namespace NaturaHub.Domain.Enums;

// Ids fixos dos status padrão — evita "números mágicos" no código
// Em vez de escrever StatusId = 1, escrevemos StatusId = (int)DefaultStatus.Active
public enum DefaultStatus
{
    Active = 1,
    Inactive = 2
}
