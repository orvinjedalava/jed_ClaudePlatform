using GameScratch.Core.Common.Players;
using GameScratch.Core.Common.Weapons;

namespace GameScratch.Core.Services;

public class PlayerService : IPlayerService
{
    void IPlayerService.ResetPlayer(Player player)
    {
        player.ClearConditions();
    }

    Player IPlayerService.CreatePlayer(RoleType roleType, string name, WeaponType weaponType)
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
}