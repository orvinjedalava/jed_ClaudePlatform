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
    [InlineData(null)]
    [InlineData(WeaponType.BareHands)]
    public void ResetPlayer_Success(WeaponType? weaponType)
    {
        Player player = new Player()
        {
            Profile = new(),
            Stats = new(),
            Equipment = new()
            {
                Weapon = WeaponBuilder.Create().FromWeaponType(WeaponType.BareHands).Build()
            }
        };
        if (weaponType == null)
            _playerService.ResetPlayer(player);
        else
            _playerService.ResetPlayer(player, weaponType.Value);

        Weapon expectedWeapon = WeaponBuilder.Create().FromWeaponType(weaponType).Build();

        Assert.True(expectedWeapon.Equals(player.Equipment.Weapon));
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
    }
}