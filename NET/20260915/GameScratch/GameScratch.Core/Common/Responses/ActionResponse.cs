using System.Text;

namespace GameScratch.Core.Common.Responses;

public class ActionResponse
{
    public string Message { get; set; } = string.Empty;

    public AttackResponse? Attack { get; set; }
    public RollIniativeResponse? RollInitiative { get; set; }
    public bool SwitchPlayerTurn { get; set; }

    public string ToConsoleString()
    {
        var sb = new StringBuilder();

        sb.AppendLine("---------------------------------");

        if (!string.IsNullOrWhiteSpace(Message))
            sb.AppendLine(Message);
        
        if (Attack != null)
        {
            sb.AppendLine($"Total ArmorClass: {Attack.DefenderArmorClass} {string.Join(" ", Attack.DefenderArmorClassModifiers?.Select(m => m.ToString("+0;-0")) ?? Enumerable.Empty<string>())}" );
            sb.AppendLine($"Total DiceRoll: {Attack.AttackerDiceRoll} {string.Join(" ", Attack.AttackerDiceRollModifiers?.Select(m => m.ToString("+0;-0")) ?? Enumerable.Empty<string>())}");
        }

        return sb.ToString();
    }
}