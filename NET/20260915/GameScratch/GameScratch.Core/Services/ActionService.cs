using System.Text;
using GameScratch.Core.Common;
using GameScratch.Core.Common.Players;

namespace GameScratch.Core.Services;

public interface IActionContext
{
    Player Attacker { get; }
    Player Defender { get; }
}

public sealed record ActionContext(Player Attacker, Player Defender) : IActionContext;

public class ActionService : IActionService
{
    private IDiceService _diceService;
    public Dictionary<string, Func<IActionContext, string>> Actions { get; init; }

    public ActionService(IDiceService diceService)
    {
        _diceService = diceService;

        Actions = new()
        {
            { nameof(Attack), ctx => Attack(ctx.Attacker, ctx.Defender) },
            { nameof(GuardStance), ctx => GuardStance(ctx.Attacker) }
        };
    }

    public string Attack(Player attacker, Player defender)
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{attacker.Profile.Name} ATTACKS {defender.Profile.Name} with {attacker.Equipment.Weapon.Name}");

        attacker.UseWeapon();
        
        int attackRoll = _diceService.Roll(DiceType.D20);
        int defenderArmorClass = defender.GetArmorClass();
        bool attackSuccessfull = attackRoll >= defenderArmorClass;

        if (attackSuccessfull)
        {
            int damage = _diceService.Roll(attacker.Equipment.Weapon.BaseDamage);
            defender.AddHitPointsDamage(damage);
            sb.Append($" and hits, doing {damage} points of damage.");
        }
        else
        {
            sb.Append($" but missed!");
        }

        sb.AppendLine();

        string rollSummaryMsg = $"ArmorClass: {defenderArmorClass}\nRoll:{attackRoll}\nHit:{attackSuccessfull}";
        sb.AppendLine(rollSummaryMsg);

        return sb.ToString();
    }

    public string GuardStance(Player attacker)
    {
        return $"{attacker.Profile.Name} goes into GUARD STANCE";
    }

    public string InvokeAction(string actionName, Player attacker, Player defender)
    {
        if (!Actions.TryGetValue(actionName, out var action))
            throw new ArgumentException($"Unknown action: {actionName}", nameof(actionName));

        IActionContext context = new ActionContext(attacker, defender);

        return action(context);
    }
}