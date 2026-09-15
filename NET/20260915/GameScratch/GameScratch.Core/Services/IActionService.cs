using GameScratch.Core.Common.Player;

namespace GameScratch.Core.Services;

public interface IActionService
{
    string Attack(Record attacker, Record defender);
}