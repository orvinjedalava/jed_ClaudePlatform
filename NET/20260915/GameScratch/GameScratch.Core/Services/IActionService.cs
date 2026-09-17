using GameScratch.Core.Common.Players;
using GameScratch.Core.Common.Responses;

namespace GameScratch.Core.Services;

public interface IActionService
{

    Dictionary<string, Func<IActionContext, ActionResponse>> Actions { get; init; }
    ActionResponse Attack(Player attacker, Player defender);
    ActionResponse Guard(Player attacker);
    ActionResponse EndTurn(Player attacker);
    ActionResponse RollIniative(Player player);
}