using System.Text;
using GameScratch.Contracts.Entities.Responses;

namespace GameScratch.Core.Extensions;

public static class PushResponseExtensions
{
    public static string ToConsoleString(this PushResponse response)
    {
        StringBuilder sb = new();

        sb.AppendLine("---------------------------------");
        sb.AppendLine("Push DiceRoll values:");
        sb.AppendLine();

        sb.AppendLine($"Total PoiseClass: {response.DefenderTotalPoiseClass} = {response.DefenderPoiseClass} {string.Join(" ", response.DefenderPoiseClassModifiers?.Select(m => m.ToString("+0;-0")) ?? Enumerable.Empty<string>())} -{response.AttackerPushModifier}" );
        sb.AppendLine($"Total Push DiceRoll: {response.AttackerTotalDiceRoll} = {response.AttackerDiceRoll} {string.Join(" ", response.AttackerDiceRollModifiers?.Select(m => m.ToString("+0;-0")) ?? Enumerable.Empty<string>())}");

        return sb.ToString();
    }
      
}