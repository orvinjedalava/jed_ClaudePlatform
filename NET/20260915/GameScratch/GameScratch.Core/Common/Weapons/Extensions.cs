namespace GameScratch.Core.Common.Weapons;

public static class WeaponExtensions
{
    public static DiceType GetHitPointsDamageDiceType(this WeaponType weaponType) => weaponType switch
    {
        WeaponType.OneHandShortSword => DiceType.D4,
        _ => throw new ArgumentOutOfRangeException(nameof(weaponType), weaponType, "Unknown weapon type.")
    };

    public static int GetStaminaPointsDamage(this WeaponType weaponType) => weaponType switch
    {
        WeaponType.OneHandShortSword => 1,
        _ => throw new ArgumentOutOfRangeException(nameof(weaponType), weaponType, "Unknown weapon type.")
    };

    public static int GetStaminaCost(this WeaponType weaponType) => weaponType switch
    {
        WeaponType.OneHandShortSword => 3,
        _ => throw new ArgumentOutOfRangeException(nameof(weaponType), weaponType, "Unknown weapon type.")
    };
} 