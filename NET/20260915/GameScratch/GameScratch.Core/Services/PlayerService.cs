using GameScratch.Core.Common.Players;
using GameScratch.Core.Common.Weapons;

namespace GameScratch.Core.Services;

public class PlayerService : IPlayerService
{
    private IActionService _actionService;
    public PlayerService(IActionService actionService)
    {
        _actionService = actionService;
    }

    public void ResetPlayer(Player player)
    {
        player.ClearConditions();
    }

    public Player CreatePlayer(RoleType roleType, string name, WeaponType weaponType)
    {
        Player player = PlayersFactory.DefaultPlayer;

        Weapon weapon = WeaponBuilder
            .Create()
            .FromWeaponType(weaponType)
            .Build();

        player.Equipment.Weapon = weapon;
        player.Profile.Name = name;
        player.Profile.RoleType = roleType;

        return player;
    }

    public string AttackChampion(Player player, Player champion)
    {
        return _actionService.Attack(player, champion);
    }

    public string GuardStance(Player player)
    {
        return _actionService.GuardStance(player);
    }
}