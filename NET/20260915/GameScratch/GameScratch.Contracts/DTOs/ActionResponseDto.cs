namespace GameScratch.Contracts.DTOs;

public record ActionResponseDto
{
    public string Message { get; set; } = string.Empty;
    public string LLMMessage { get; set; } = string.Empty;
    public char? LLMActionChoice { get; set; }

    public AttackResponseDto? Attack { get; set; }
    public RollInitiativeResponseDto? RollInitiative { get; set; }
    public bool SwitchPlayerTurn { get; set; }
}