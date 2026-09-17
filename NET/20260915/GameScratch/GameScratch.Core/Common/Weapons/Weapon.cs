using System.ComponentModel.DataAnnotations;

namespace GameScratch.Core.Common.Weapons;

public class Weapon
{
    public required DiceType HitPointsDamageDiceType { get; init; }
    public required int StaminaPointsDamage { get; init; }
    public required string Name { get; init; }
    public required int StaminaCost { get; set; }
    public required WeaponType WeaponType { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;
        if (obj is not Weapon weapon)
            return false;

        return HitPointsDamageDiceType == weapon.HitPointsDamageDiceType
            && StaminaPointsDamage == weapon.StaminaPointsDamage
            && Name == weapon.Name
            && StaminaCost == weapon.StaminaCost
            && WeaponType == weapon.WeaponType;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}