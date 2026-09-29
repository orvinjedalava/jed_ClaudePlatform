using System.Text;
using GameScratch.Contracts.Entities.Responses;

namespace GameScratch.Core.Extensions;

public static class AttackResponseExtensions
{
    public static string ToConsoleString(this AttackResponse response)
    {
        StringBuilder sb = new();

        sb.AppendLine("---------------------------------");
        sb.AppendLine("Attack DiceRoll values:");
        sb.AppendLine();

        sb.AppendLine($"Total ArmorClass: {response.DefenderTotalArmorClass} = {response.DefenderArmorClass} {string.Join(" ", response.DefenderArmorClassModifiers?.Select(m => m.ToString("+0;-0")) ?? Enumerable.Empty<string>())}" );
        sb.AppendLine($"Total Attack DiceRoll: {response.AttackerTotalDiceRoll} = {response.AttackerDiceRoll} {string.Join(" ", response.AttackerDiceRollModifiers?.Select(m => m.ToString("+0;-0")) ?? Enumerable.Empty<string>())}");

        if (response.Counter != null)
        {
            sb.AppendLine();
            sb.AppendLine(response.Counter.ToConsoleString());
        }
        if (response.Push != null)
        {
            sb.AppendLine();
            sb.AppendLine(response.Push.ToConsoleString());
        }

        return sb.ToString();
    }
}