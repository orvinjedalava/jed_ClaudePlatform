using System.ComponentModel.DataAnnotations;

namespace GameScratch.Core.Common.Weapons;

public class Weapon
{
    public DiceType BaseDamage { get; init; }
    public string Name { get; init; }
}