using GameScratch.Core.Common.Players;
using GameScratch.Core.Common.Weapons;
using GameScratch.Core.Services;
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
        Weapon weapon = WeaponBuilder.Create().FromWeaponType(WeaponType.OneHandShortSword).Build();

        Player attacker = PlayersFactory.DefaultChallengerPlayer;
        Player defender = PlayersFactory.DefaultChampionPlayer;

        Assert.NotNull(_actionService.Attack(attacker, defender));
    }

    [Fact]
    public void GuardStance_Success()
    {
        Weapon weapon = WeaponBuilder.Create().FromWeaponType(WeaponType.OneHandShortSword).Build();

        Player attacker = PlayersFactory.DefaultChallengerPlayer;

        Assert.NotNull(_actionService.Guard(attacker));
    }
}