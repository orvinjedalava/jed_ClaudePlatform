using GameScratch.Core.Common;
using GameScratch.Core.Common.Weapons;
using GameScratch.Core.Common.Players;

namespace GameScratch.Tests.Common.Players;

public class FactoryTests
{
    [Theory]
    [InlineData(WeaponType.OneHandShortSword, RoleType.Challenger)]
    [InlineData(WeaponType.OneHandShortSword, RoleType.Champion)]
    public void BuildPlayer_Success(WeaponType weaponType, RoleType roleType)
    {
        Player player = PlayersFactory.DefaultPlayer;

        string name = roleType.ToString();

        Weapon weapon = WeaponBuilder
            .Create()
            .FromWeaponType(weaponType)
            .Build();

        player.Equipment.Weapon = weapon;
        player.Profile.RoleType = roleType;
        player.Profile.Name = name;
        
        Assert.Equal(roleType, player.Profile.RoleType);
        Assert.Equal(name, player.Profile.Name);
        Assert.Equal(20, player.Stats.HitPoints);
        Assert.Equal(10, player.Stats.StaminaPoints);
        Assert.Equal(10, player.Stats.ArmorClass);
        Assert.Equal(StanceType.Neutral, player.Conditions.StanceType);
    }
}