using System.ComponentModel.DataAnnotations;

namespace GameScratch.Core.Common.Weapons;

public class Weapon
{
    public required DiceType BaseDamage { get; init; }
    public required string Name { get; init; }
    public required int StaminaCost { get; set; }
}