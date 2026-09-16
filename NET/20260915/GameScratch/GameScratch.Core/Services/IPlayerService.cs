using GameScratch.Core.Common.Players;
using GameScratch.Core.Common.Weapons;

namespace GameScratch.Core.Services;

public interface IPlayerService
{
    void ResetPlayer(Player player);

    Player CreatePlayer(RoleType roleType, string name, WeaponType weaponType);

    string AttackChampion(Player player, Player champion);
    string GuardStance(Player player);
}