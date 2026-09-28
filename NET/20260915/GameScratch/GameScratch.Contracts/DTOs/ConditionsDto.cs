namespace GameScratch.Contracts.DTOs;

public record ConditionsDto
{
    public required string StanceType { get; set; }
    public int HitPointsDamage { get; set; }
    public int StaminaPointsDamage { get;set; }
}