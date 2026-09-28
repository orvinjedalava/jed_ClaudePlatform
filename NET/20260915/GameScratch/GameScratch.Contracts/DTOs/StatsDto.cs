namespace GameScratch.Contracts.DTOs;

public record StatsDto
{
    public int HitPoints { get; set; } = 0;
    public int StaminaPoints { get; set; } = 0;
    public int ArmorClass { get; set; } = 0;
    public int BalanceClass { get; set; } = 0;
    public int PoiseClass { get; set; } = 0;
}