namespace GameScratch.Core.Common.Responses;

public class ActionResponse
{
    public string Message { get; set; } = string.Empty;

    public string TargetName { get; set; } = string.Empty;
    public int TargetValue { get; set; }
    public List<int>? TargetValueModifiers { get; set; }    
    public int DiceRoll { get; set; }
    public bool SwitchPlayerTurn { get; set; }
}