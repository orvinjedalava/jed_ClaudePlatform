namespace GameScratch.Core.Services;

public enum DiceType : int
{
    D4 = 4,
    D6 = 6,
    D8 = 8,
    D12 = 12,
    D20 = 20
}

public class DiceService: IDiceService
{
    int IDiceService.Roll(DiceType diceType)
    {
        int maxNoOfSides = (int)diceType;
        
        // inclusive of minimum value, but exclusive of maximum value, hence the need to add +1 for the max value.
        return Random.Shared.Next(1, maxNoOfSides + 1);
    }
}