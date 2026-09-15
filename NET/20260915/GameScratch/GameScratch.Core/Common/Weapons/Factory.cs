namespace GameScratch.Core.Common.Weapons;

public static class Factory
{
    public static Weapon BuildWeapon(WeaponType weaponType)
    {
        return new()
        {
            Name = weaponType.ToString(),
            BaseDamage = weaponType.GetDiceType()
        };
    }

    public static DiceType GetDiceType(this WeaponType weaponType) => weaponType switch
    {
        WeaponType.BareHands => DiceType.D4,
        _ => throw new ArgumentOutOfRangeException(nameof(weaponType), weaponType, "Unknown weapon type.")
    };
    
}