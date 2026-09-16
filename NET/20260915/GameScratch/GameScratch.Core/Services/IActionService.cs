using GameScratch.Core.Common.Players;

namespace GameScratch.Core.Services;

public interface IActionService
{

    Dictionary<string, Func<IActionContext, string>> Actions { get; init; }
    string Attack(Player attacker, Player defender);
    string GuardStance(Player attacker);
}