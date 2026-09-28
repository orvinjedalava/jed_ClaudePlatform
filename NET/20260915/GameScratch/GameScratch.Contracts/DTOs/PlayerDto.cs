namespace GameScratch.Contracts.DTOs;

public record PlayerDto
{
    public required ProfileDto Profile { get; init; }
    public required StatsDto Stats { get; init; }
    public required EquipmentDto Equipment { get; init; }
    public required ConditionsDto Conditions { get; set; }
}