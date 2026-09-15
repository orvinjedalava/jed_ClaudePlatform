using GameScratch.Core.Services;
using GameScratch.Core.Common;

namespace GameScratch.Tests.Services;

public class DiceServiceTests
{
    private readonly IDiceService _diceService;

    public DiceServiceTests()
    {
        _diceService = new DiceService();
    }

    [Theory]
    [InlineData(DiceType.D4, 1, 4)]
    [InlineData(DiceType.D6, 1, 6)]
    [InlineData(DiceType.D8, 1, 8)]
    [InlineData(DiceType.D12, 1, 12)]
    [InlineData(DiceType.D20, 1, 20)]
    public void Roll_Success(DiceType diceType, int minValue, int maxValue)
    {
        int result = _diceService.Roll(diceType);

        Assert.True(minValue <= result && result <= maxValue);
    }
}