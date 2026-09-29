using System.Text;
using GameScratch.Contracts.Entities.Responses;

namespace GameScratch.Core.Extensions;

public static class CounterResponseExtensions
{
    public static string ToConsoleString(this CounterResponse response)
    {
        StringBuilder sb = new();

        sb.AppendLine("---------------------------------");
        sb.AppendLine("Counter DiceRoll values:");
        sb.AppendLine();

        sb.AppendLine($"Total BalanceClass: {response.AttackerTotalBalanceClass} = {response.AttackerBalanceClass} {string.Join(" ", response.AttackerBalanceClassModifiers?.Select(m => m.ToString("+0;-0")) ?? Enumerable.Empty<string>())} -{response.AttackerMissModifier}" );
        sb.AppendLine($"Total Counter DiceRoll: {response.DefenderTotalDiceRoll} = {response.DefenderDiceRoll} {string.Join(" ", response.DefenderDiceRollModifiers?.Select(m => m.ToString("+0;-0")) ?? Enumerable.Empty<string>())}");

        return sb.ToString();
    }
}