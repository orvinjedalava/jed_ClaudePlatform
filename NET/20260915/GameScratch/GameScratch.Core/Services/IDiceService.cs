using GameScratch.Contracts.Enums;

namespace GameScratch.Core.Services;

public interface IDiceService
{
    int Roll(DiceType diceType);
}