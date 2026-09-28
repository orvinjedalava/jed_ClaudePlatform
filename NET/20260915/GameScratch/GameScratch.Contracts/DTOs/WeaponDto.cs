namespace GameScratch.Contracts.DTOs;

public record WeaponDto
{
    public required string HitPointsDamageDiceType { get; init; }
    public required int StaminaPointsDamage { get; init; }
    public required string Name { get; init; }
    public required int StaminaCost { get; set; }
    public required int PoiseDamageModifier { get; set; }
    public required string WeaponType { get; set; }
}