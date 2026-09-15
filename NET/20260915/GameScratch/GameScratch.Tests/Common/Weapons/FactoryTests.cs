using GameScratch.Core.Common;
using GameScratch.Core.Common.Weapons;

namespace GameScratch.Tests.Common.Weapons;

public class FactoryTests
{
    [Theory]
    [InlineData(WeaponType.BareHands, "BareHands", DiceType.D4)]
    public void BuildWeapon_Success(WeaponType weaponType, string name, DiceType diceType)
    {
        Weapon result = Factory.BuildWeapon(weaponType);

        Assert.Equal(diceType, result.BaseDamage);
        Assert.Equal(name, result.Name, ignoreCase: true);
    }
}