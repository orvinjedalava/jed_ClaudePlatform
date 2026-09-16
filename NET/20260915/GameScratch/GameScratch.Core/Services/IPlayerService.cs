using GameScratch.Core.Common.Players;
using GameScratch.Core.Common.Weapons;

namespace GameScratch.Core.Services;

public interface IPlayerService
{
    void ResetPlayer(Player player);
    void ResetPlayer(Player player, WeaponType weaponType);
}