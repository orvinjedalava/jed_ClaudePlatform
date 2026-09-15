namespace GameScratch.Core.Common.Weapons;

public static class WeaponExtensions
{
    public static DiceType GetDiceType(this WeaponType weaponType) => weaponType switch
    {
        WeaponType.BareHands => DiceType.D4,
        _ => throw new ArgumentOutOfRangeException(nameof(weaponType), weaponType, "Unknown weapon type.")
    };

    public static int GetStaminaCost(this WeaponType weaponType) => weaponType switch
    {
        WeaponType.BareHands => 10,
        _ => throw new ArgumentOutOfRangeException(nameof(weaponType), weaponType, "Unknown weapon type.")
    };
} 