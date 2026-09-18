using GameScratch.Core.Common;
using GameScratch.Core.Common.Weapons;

namespace GameScratch.Tests.Common.Weapons;

public class FactoryTests
{
    [Theory]
    [InlineData(null, DiceType.D4, 3)]
    [InlineData(WeaponType.ShortSword, DiceType.D4, 3)]
    public void BuildWeapon_Success(WeaponType? weaponType, DiceType expectedHitPointsDamageDiceType, int expectedStaminaCost)
    {
        string expectedName = weaponType?.ToString() ?? WeaponType.ShortSword.ToString();
        Weapon result = WeaponBuilder
            .Create()
            .FromWeaponType(weaponType)
            .Build();

        Assert.Equal(expectedHitPointsDamageDiceType, result.HitPointsDamageDiceType);
        Assert.Equal(expectedName, result.Name, ignoreCase: true);
        Assert.Equal(expectedStaminaCost, result.StaminaCost);
    }
}