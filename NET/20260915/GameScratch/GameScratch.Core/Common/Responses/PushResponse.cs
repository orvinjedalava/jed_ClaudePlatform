using System.Text;

namespace GameScratch.Core.Common.Responses;

public class PushResponse
{
    public int DefenderPoiseClass { get; set; }
    public int DefenderTotalPoiseClass { get; set; }
    public List<int>? DefenderPoiseClassModifiers { get; set; }
    public int AttackerPushModifier { get; set; } 

    public int AttackerDiceRoll { get; set; }
    public int AttackerTotalDiceRoll { get; set; }
    public List<int>? AttackerDiceRollModifiers { get; set; }

    public string ToConsoleString()
    {
        StringBuilder sb = new();

        sb.AppendLine("---------------------------------");
        sb.AppendLine("Push DiceRoll values:");
        sb.AppendLine();

        sb.AppendLine($"Total PoiseClass: {DefenderTotalPoiseClass} = {DefenderPoiseClass} {string.Join(" ", DefenderPoiseClassModifiers?.Select(m => m.ToString("+0;-0")) ?? Enumerable.Empty<string>())} -{AttackerPushModifier}" );
        sb.AppendLine($"Total Push DiceRoll: {AttackerTotalDiceRoll} = {AttackerDiceRoll} {string.Join(" ", AttackerDiceRollModifiers?.Select(m => m.ToString("+0;-0")) ?? Enumerable.Empty<string>())}");

        return sb.ToString();
    }
}