using System.ComponentModel.DataAnnotations;

namespace GameScratch.Core.Common.Weapons;

public class Weapon
{
    public required DiceType BaseDamage { get; init; }
    public required string Name { get; init; }
    public required int StaminaCost { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        if (obj is not Weapon weapon)
            return false;

        return BaseDamage == weapon.BaseDamage
            && Name == weapon.Name
            && StaminaCost == weapon.StaminaCost;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}