using System.Text;

namespace GameScratch.Core.Common.Responses;

public class ActionResponse
{
    public string Message { get; set; } = string.Empty;
    public string LLMMessage { get; set; } = string.Empty;

    public AttackResponse? Attack { get; set; }
    public RollIniativeResponse? RollInitiative { get; set; }
    public bool SwitchPlayerTurn { get; set; }

    public string ToConsoleString()
    {
        var sb = new StringBuilder();

        sb.AppendLine("---------------------------------");
        if (!string.IsNullOrWhiteSpace(LLMMessage))
        {
            sb.AppendLine(LLMMessage);
            sb.AppendLine();
        }

        if (!string.IsNullOrWhiteSpace(Message))
            sb.AppendLine(Message);
        
        if (Attack != null)
        {
            sb.AppendLine();
            sb.AppendLine(Attack.ToConsoleString());
        }

        return sb.ToString();
    }
}