namespace GameScratch.Contracts.DTOs;

public record PushResponseDto
{
    public int DefenderPoiseClass { get; set; }
    public int DefenderTotalPoiseClass { get; set; }
    public List<int>? DefenderPoiseClassModifiers { get; set; }
    public int AttackerPushModifier { get; set; } 

    public int AttackerDiceRoll { get; set; }
    public int AttackerTotalDiceRoll { get; set; }
    public List<int>? AttackerDiceRollModifiers { get; set; }
}