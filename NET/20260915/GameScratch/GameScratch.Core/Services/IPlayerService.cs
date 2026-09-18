using GameScratch.Core.Common.Responses;
using GameScratch.Core.Common.Players;
using GameScratch.Core.Common.Weapons;

namespace GameScratch.Core.Services;

public interface IPlayerService
{
    void ResetPlayer(Player player);

    Player CreatePlayer(RoleType roleType, string name, WeaponType weaponType);

    ActionResponse StartPlayerTurn(Player player);

    ActionResponse Attack(Player attacker, Player defender);
    ActionResponse Guard(Player attacker, Player defender);
    ActionResponse Wait(Player attacker, Player defender);
    ActionResponse InvokeRandomAction(Player attacker, Player defender); 
    (ActionResponse, Player) RollIniative(Player challenger, Player champion);
}