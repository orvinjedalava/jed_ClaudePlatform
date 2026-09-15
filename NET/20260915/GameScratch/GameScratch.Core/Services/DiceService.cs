using GameScratch.Core.Common;

namespace GameScratch.Core.Services;

public class DiceService: IDiceService
{
    int IDiceService.Roll(DiceType diceType)
    {
        int maxNoOfSides = (int)diceType;

        // inclusive of minimum value, but exclusive of maximum value, hence the need to add +1 for the max value.
        return Random.Shared.Next(1, maxNoOfSides + 1);
    }
}