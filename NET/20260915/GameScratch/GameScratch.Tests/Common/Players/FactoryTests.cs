using GameScratch.Core.Common;
using GameScratch.Core.Common.Weapons;
using GameScratch.Core.Common.Players;

namespace GameScratch.Tests.Common.Players;

public class FactoryTests
{
    [Theory]
    [InlineData(WeaponType.BareHands, RoleType.Challenger, "Player")]
    [InlineData(WeaponType.BareHands, RoleType.Champion, "Boss")]
    public void BuildPlayer_Success(WeaponType? weaponType, RoleType roleType, string name)
    {
        Weapon weapon = WeaponBuilder
            .Create()
            .FromWeaponType(weaponType)
            .Build();
        
        Player result = PlayerBuilder
            .Create()
            .WithProfile(roleType, name)
            .WithStats()
            .WithEquipment(weapon)
            .Build();

        Assert.Equal(roleType, result.Profile.RoleType);
        Assert.Equal(name, result.Profile.Name);
        Assert.Equal(20, result.Stats.HitPointsCurrent);
        Assert.Equal(20, result.Stats.HitPointsMax);
        Assert.Equal(10, result.Stats.StaminaPointsCurrent);
        Assert.Equal(10, result.Stats.StaminaPointsMax);
        Assert.Equal(StanceType.Default, result.StanceType);
    }
}