using System.Text;

namespace GameScratch.Core.Common.Responses;

public class AttackResponse
{
    public int DefenderArmorClass { get; set; }
    public int DefenderTotalArmorClass { get; set; }
    public List<int>? DefenderArmorClassModifiers { get; set; }

    public int AttackerDiceRoll { get; set; }
    public int AttackerTotalDiceRoll { get; set; }
    public List<int>? AttackerDiceRollModifiers { get; set; }

    public string ToConsoleString()
    {
        StringBuilder sb = new();

        sb.AppendLine($"Total ArmorClass: {DefenderTotalArmorClass} = {DefenderArmorClass} {string.Join(" ", DefenderArmorClassModifiers?.Select(m => m.ToString("+0;-0")) ?? Enumerable.Empty<string>())}" );
        sb.AppendLine($"Total DiceRoll: {AttackerTotalDiceRoll} = {AttackerDiceRoll} {string.Join(" ", AttackerDiceRollModifiers?.Select(m => m.ToString("+0;-0")) ?? Enumerable.Empty<string>())}");

        return sb.ToString();
    }
}