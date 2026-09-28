namespace GameScratch.Contracts.DTOs;

public record AttackResponseDto
{
    public int DefenderArmorClass { get; set; }
    public int DefenderTotalArmorClass { get; set; }
    public List<int>? DefenderArmorClassModifiers { get; set; }

    public int AttackerDiceRoll { get; set; }
    public int AttackerTotalDiceRoll { get; set; }
    public List<int>? AttackerDiceRollModifiers { get; set; }

    public CounterResponseDto? Counter { get; set; }
    public PushResponseDto? Push { get; set; }
}