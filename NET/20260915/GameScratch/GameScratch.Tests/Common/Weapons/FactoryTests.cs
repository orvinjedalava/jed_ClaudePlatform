using GameScratch.Core.Common;
using GameScratch.Core.Common.Weapons;

namespace GameScratch.Tests.Common.Weapons;

public class FactoryTests
{
    [Theory]
    [InlineData(null, "BareHands", DiceType.D4, 2)]
    [InlineData(WeaponType.BareHands, "BareHands", DiceType.D4, 2)]
    public void BuildWeapon_Success(WeaponType? weaponType, string expectedName, DiceType expectedDiceType, int expectedStaminaCost)
    {
        Weapon result = WeaponBuilder
            .Create()
            .FromWeaponType(weaponType)
            .Build();

        Assert.Equal(expectedDiceType, result.BaseDamage);
        Assert.Equal(expectedName, result.Name, ignoreCase: true);
        Assert.Equal(expectedStaminaCost, result.StaminaCost);
    }
}