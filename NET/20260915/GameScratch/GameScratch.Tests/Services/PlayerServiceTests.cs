using GameScratch.Core.Common.Weapons;
using GameScratch.Core.Services;
using GameScratch.Core.Common.Players;
using Moq;

namespace GameScratch.Tests.Services;

public class PlayerServiceTests
{
    private readonly IPlayerService _playerService;

    private readonly Mock<IActionService> _actionServiceMock;

    public PlayerServiceTests()
    {
        _actionServiceMock = new Mock<IActionService>();

        _playerService = new PlayerService(_actionServiceMock.Object);
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
        Assert.Equal(StanceType.Neutral, player.Conditions.StanceType);
    }

    [Theory]
    [InlineData(RoleType.Challenger, WeaponType.OneHandShortSword)]
    [InlineData(RoleType.Champion, WeaponType.OneHandShortSword)]
    public void CreatePlayer_Success(RoleType roleType, WeaponType weaponType)
    {
        string name = roleType.ToString();

        Weapon weapon = WeaponBuilder
            .Create()
            .FromWeaponType(WeaponType.OneHandShortSword)
            .Build();
        
        Player result = _playerService.CreatePlayer(roleType, name, weaponType);

        Assert.Equal(roleType, result.Profile.RoleType);
        Assert.Equal(name, result.Profile.Name);
        Assert.True(result.Equipment.Weapon.Equals(weapon));
        Assert.Equal(StanceType.Neutral, result.Conditions.StanceType);
    }

    [Fact]
    public void AttackChampion_Success()
    {
        Player challenger = PlayersFactory.DefaultChallengerPlayer;
        Player champion = PlayersFactory.DefaultChampionPlayer;
        var exception = Record.Exception(() => _playerService.Attack(challenger, champion));

        Assert.Null(exception);
    }

    [Fact]
    public void GuardStance_Success()
    {
        Player challenger = PlayersFactory.DefaultChallengerPlayer;
        var exception = Record.Exception(() => _playerService.Guard(challenger));

        Assert.Null(exception);
    }

    [Fact]
    public void StartPlayerTurn_Success()
    {
        Player challenger = PlayersFactory.DefaultChallengerPlayer;
        var exception = Record.Exception(() => _playerService.StartPlayerTurn(challenger));

        Assert.Null(exception);
    }
}