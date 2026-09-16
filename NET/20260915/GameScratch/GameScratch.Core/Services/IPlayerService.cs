using GameScratch.Core.Common.Players;
using GameScratch.Core.Common.Weapons;

namespace GameScratch.Core.Services;

public interface IPlayerService
{
    void ResetPlayer(Player record);
    void ResetPlayer(Player record, WeaponType weaponType);
}