using System.Text;

namespace GameScratch.Contracts.Entities.Responses;

public class AttackResponse
{
    public int DefenderArmorClass { get; set; }
    public int DefenderTotalArmorClass { get; set; }
    public List<int>? DefenderArmorClassModifiers { get; set; }

    public int AttackerDiceRoll { get; set; }
    public int AttackerTotalDiceRoll { get; set; }
    public List<int>? AttackerDiceRollModifiers { get; set; }

    public CounterResponse? Counter { get; set; }
    public PushResponse? Push { get; set; }

    
}