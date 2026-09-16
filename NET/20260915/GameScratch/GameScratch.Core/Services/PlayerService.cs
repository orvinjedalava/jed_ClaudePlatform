using GameScratch.Core.Common.Players;
using GameScratch.Core.Common.Weapons;

namespace GameScratch.Core.Services;

public class PlayerService : IPlayerService
{
    void IPlayerService.ResetPlayer(Player player)
    {
        ResetPlayer(player, WeaponType.BareHands);
    }

    void IPlayerService.ResetPlayer(Player player, WeaponType weaponType)
    {
        ResetPlayer(player, weaponType);
    }

    private void ResetPlayer(Player player, WeaponType weaponType)
    {
        Weapon weapon = WeaponBuilder
            .Create()
            .FromWeaponType(weaponType)
            .Build();

        player = PlayerBuilder
            .Create()
            .WithProfile(player.Profile.RoleType, player.Profile.Name)
            .WithStats()
            .WithEquipment(weapon)
            .Build();
    }
}