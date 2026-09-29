namespace GameScratch.Contracts.Entities.Responses;

public class ActionResponse
{
    public string Message { get; set; } = string.Empty;
    public string LLMMessage { get; set; } = string.Empty;
    public char? LLMActionChoice { get; set; }

    public AttackResponse? Attack { get; set; }
    public RollInitiativeResponse? RollInitiative { get; set; }
    public bool SwitchPlayerTurn { get; set; }
}