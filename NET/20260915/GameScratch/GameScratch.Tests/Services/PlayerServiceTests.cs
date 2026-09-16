using GameScratch.Core.Common.Weapons;
using GameScratch.Core.Services;
using GameScratch.Core.Common.Players;

namespace GameScratch.Tests.Services;

public class PlayerServiceTests
{
    private readonly IPlayerService _playerService;

    public PlayerServiceTests()
    {
        _playerService = new PlayerService();
    }

    [Theory]
    [InlineData(RoleType.Challenger)]
    [InlineData(RoleType.Champion)]
    public void ResetPlayer_Success(RoleType roleType)
    {
        string name = roleType.ToString();

        Player player = PlayersFactory.DefaultPlayer;
        player.Profile.Name = name;
        player.Profile.RoleType = roleType;

        player.AddHitPointsDamage(10);
        player.UseWeapon();

        _playerService.ResetPlayer(player);

        Assert.Equal(roleType, player.Profile.RoleType);
        Assert.Equal(name, player.Profile.Name);
        Assert.Equal(player.Stats.HitPoints, player.GetHitPointsRemaining());
        Assert.Equal(player.Stats.StaminaPoints, player.GetStaminaPointsRemaining());
        Assert.Equal(StanceType.Default, player.Conditions.StanceType);
    }

    [Theory]
    [InlineData(RoleType.Challenger, WeaponType.BareHands)]
    [InlineData(RoleType.Champion, WeaponType.BareHands)]
    public void CreatePlayer_Success(RoleType roleType, WeaponType weaponType)
    {
        string name = roleType.ToString();

        Weapon weapon = WeaponBuilder
            .Create()
            .FromWeaponType(WeaponType.BareHands)
            .Build();
        
        Player result = _playerService.CreatePlayer(roleType, name, weaponType);

        Assert.Equal(roleType, result.Profile.RoleType);
        Assert.Equal(name, result.Profile.Name);
        Assert.True(result.Equipment.Weapon.Equals(weapon));
        Assert.Equal(StanceType.Default, result.Conditions.StanceType);
    }
}