using System.Text;

namespace GameScratch.Core.Common.Responses;

public class CounterResponse
{
    public int AttackerBalanceClass { get; set; }
    public int AttackerTotalBalanceClass { get; set; }
    public List<int>? AttackerBalanceClassModifiers { get; set; }
    public int AttackerMissModifier { get; set; } 

    public int DefenderDiceRoll { get; set; }
    public int DefenderTotalDiceRoll { get; set; }
    public List<int>? DefenderDiceRollModifiers { get; set; }

    public string ToConsoleString()
    {
        StringBuilder sb = new();

        sb.AppendLine("---------------------------------");
        sb.AppendLine("Counter DiceRoll values:");
        sb.AppendLine();

        sb.AppendLine($"Total BalanceClass: {AttackerTotalBalanceClass} = {AttackerBalanceClass} {string.Join(" ", AttackerBalanceClassModifiers?.Select(m => m.ToString("+0;-0")) ?? Enumerable.Empty<string>())} -{AttackerMissModifier}" );
        sb.AppendLine($"Total Counter DiceRoll: {DefenderTotalDiceRoll} = {DefenderDiceRoll} {string.Join(" ", DefenderDiceRollModifiers?.Select(m => m.ToString("+0;-0")) ?? Enumerable.Empty<string>())}");

        return sb.ToString();
    }
}