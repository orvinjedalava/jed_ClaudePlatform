using GameScratch.Contracts.Entities.Players;
using GameScratch.Core.Responses;

namespace GameScratch.Core.Services;

public interface IActionService
{

    Dictionary<string, Func<IActionContext, ActionResponse>> Actions { get; init; }
    ActionResponse Attack(Player attacker, Player defender);
    ActionResponse Guard(Player attacker, Player defender);
    ActionResponse Wait(Player attacker, Player defender);
    ActionResponse RollIniative(Player player);
}