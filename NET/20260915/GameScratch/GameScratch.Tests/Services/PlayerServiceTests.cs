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
    [InlineData(null, RoleType.Challenger, "Player")]
    [InlineData(WeaponType.BareHands, RoleType.Champion, "Model")]
    public void ResetPlayer_Success(WeaponType? weaponType, RoleType roleType, string name)
    {
        Weapon weapon = WeaponBuilder.Create().FromWeaponType(weaponType).Build();

        Player player = PlayerBuilder.Create().WithProfile(roleType, name).WithStats().WithEquipment(weapon).Build();
        if (weaponType == null)
            _playerService.ResetPlayer(player);
        else
            _playerService.ResetPlayer(player, weaponType.Value);

        Assert.Equal(roleType, player.Profile.RoleType);
        Assert.Equal(name, player.Profile.Name);
        Assert.True(player.Equipment.Weapon.Equals(weapon));
        Assert.Equal(StanceType.Default, player.StanceType);
    }

    [Theory]
    [InlineData(RoleType.Challenger, "Player", WeaponType.BareHands)]
    [InlineData(RoleType.Champion, "Model", WeaponType.BareHands)]
    public void CreatePlayer_Success(RoleType roleType, string name, WeaponType weaponType)
    {
        Weapon weapon = WeaponBuilder
            .Create()
            .FromWeaponType(WeaponType.BareHands)
            .Build();
        
        Player result = _playerService.CreatePlayer(roleType, name, weaponType);

        Assert.Equal(roleType, result.Profile.RoleType);
        Assert.Equal(name, result.Profile.Name);
        Assert.True(result.Equipment.Weapon.Equals(weapon));
        Assert.Equal(StanceType.Default, result.StanceType);
    }
}