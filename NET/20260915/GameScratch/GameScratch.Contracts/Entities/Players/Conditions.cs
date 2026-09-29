using GameScratch.Contracts.Enums;

namespace GameScratch.Contracts.Entities.Players;

public class Conditions
{
    public StanceType StanceType { get; set; } = StanceType.Neutral;
    public int HitPointsDamage { get; set; }
    public int StaminaPointsDamage { get;set; }
}