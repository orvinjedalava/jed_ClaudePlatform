using GameScratch.Core.Factories;
using GameScratch.Contracts.Enums;
using GameScratch.Core.Services;
using GameScratch.Contracts.Entities.Weapons;
using GameScratch.Contracts.Entities.Players;
using Moq;

namespace GameScratch.Tests.Services;

public class ActionServiceTests
{
    private IActionService _actionService;

    private Mock<IDiceService> _diceServiceMock;

    public ActionServiceTests()
    {
        _diceServiceMock = new Mock<IDiceService>();

        _actionService = new ActionService(_diceServiceMock.Object);
    }

    [Fact]
    public void Attack_Success()
    {
        Weapon weapon = WeaponBuilder.Create().FromWeaponType(WeaponType.ShortSword).Build();

        Player attacker = PlayersFactory.DefaultChallengerPlayer;
        Player defender = PlayersFactory.DefaultChampionPlayer;

        Assert.NotNull(_actionService.Attack(attacker, defender));
    }

    [Fact]
    public void GuardStance_Success()
    {
        Weapon weapon = WeaponBuilder.Create().FromWeaponType(WeaponType.ShortSword).Build();

        Player attacker = PlayersFactory.DefaultChallengerPlayer;
        Player defender = PlayersFactory.DefaultChampionPlayer;

        Assert.NotNull(_actionService.Guard(attacker, defender));
    }
}