namespace GameScratch.Contracts.DTOs;

public record CounterResponseDto
{
    public int AttackerBalanceClass { get; set; }
    public int AttackerTotalBalanceClass { get; set; }
    public List<int>? AttackerBalanceClassModifiers { get; set; }
    public int AttackerMissModifier { get; set; } 

    public int DefenderDiceRoll { get; set; }
    public int DefenderTotalDiceRoll { get; set; }
    public List<int>? DefenderDiceRollModifiers { get; set; }
}