using GameScratch.Core.Common.Players;
using GameScratch.Core.Common.Weapons;

namespace GameScratch.Core.Services;

public class PlayerService : IPlayerService
{
    void IPlayerService.ResetPlayer(Player player)
    {
        ResetPlayer(player, player.Equipment.Weapon.WeaponType);
    }

    void IPlayerService.ResetPlayer(Player player, WeaponType weaponType)
    {
        ResetPlayer(player, weaponType);
    }

    Player IPlayerService.CreatePlayer(RoleType roleType, string name, WeaponType weaponType)
    {
        Weapon weapon = WeaponBuilder
            .Create()
            .FromWeaponType(weaponType)
            .Build();

        return PlayerBuilder
            .Create()
            .WithProfile(roleType, name)
            .WithStats()
            .WithEquipment(weapon)
            .Build();
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