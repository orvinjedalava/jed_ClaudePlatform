using GameScratch.Core.Common.Player;
using GameScratch.Core.Common.Weapons;

namespace GameScratch.Core.Services;

public class PlayerService : IPlayerService
{
    void IPlayerService.ResetPlayer(Player record)
    {
        ResetPlayer(record, WeaponType.BareHands);
    }

    void IPlayerService.ResetPlayer(Player record, WeaponType weaponType)
    {
        ResetPlayer(record, weaponType);
    }

    private void ResetPlayer(Player record, WeaponType weaponType)
    {
        record = new Player
        {
            Profile = new(),
            Stats = new(),
            Equipment = new()
            {
                Weapon = WeaponBuilder.Create().FromWeaponType(weaponType).Build()
            }
        };
    }
}