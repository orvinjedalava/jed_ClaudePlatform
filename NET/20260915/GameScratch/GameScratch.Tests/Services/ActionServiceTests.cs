using GameScratch.Core.Common.Players;
using GameScratch.Core.Common.Weapons;
using GameScratch.Core.Services;

namespace GameScratch.Tests.Services;

public class ActionServiceTests
{
    private IActionService _actionService;

    public ActionServiceTests()
    {
        _actionService = new ActionService();
    }

    [Fact]
    public void Attack_Success()
    {
        Weapon weapon = WeaponBuilder.Create().FromWeaponType(WeaponType.BareHands).Build();

        Player attacker = PlayerBuilder.Create().WithProfile(RoleType.Challenger, "Player").WithStats().WithEquipment(weapon).Build();
        Player defender = PlayerBuilder.Create().WithProfile(RoleType.Champion, "Model").WithStats().WithEquipment(weapon).Build();

        Assert.NotNull(_actionService.Attack(attacker, defender));
    }

    [Fact]
    public void GuardStance_Success()
    {
        Weapon weapon = WeaponBuilder.Create().FromWeaponType(WeaponType.BareHands).Build();

        Player attacker = PlayerBuilder.Create().WithProfile(RoleType.Challenger, "Player").WithStats().WithEquipment(weapon).Build();

        Assert.NotNull(_actionService.GuardStance(attacker));
    }
}