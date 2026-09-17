using GameScratch.Core.Common;
using GameScratch.Core.Common.Weapons;

namespace GameScratch.Tests.Common.Weapons;

public class FactoryTests
{
    [Theory]
    [InlineData(null, DiceType.D6, 3)]
    [InlineData(WeaponType.OneHandShortSword, DiceType.D6, 3)]
    public void BuildWeapon_Success(WeaponType? weaponType, DiceType expectedHitPointsDamageDiceType, int expectedStaminaCost)
    {
        string expectedName = weaponType?.ToString() ?? WeaponType.OneHandShortSword.ToString();
        Weapon result = WeaponBuilder
            .Create()
            .FromWeaponType(weaponType)
            .Build();

        Assert.Equal(expectedHitPointsDamageDiceType, result.HitPointsDamageDiceType);
        Assert.Equal(expectedName, result.Name, ignoreCase: true);
        Assert.Equal(expectedStaminaCost, result.StaminaCost);
    }
}