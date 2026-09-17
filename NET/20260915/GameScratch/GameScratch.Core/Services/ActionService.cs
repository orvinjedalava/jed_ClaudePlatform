using System.Text;
using GameScratch.Core.Common;
using GameScratch.Core.Common.Players;
using GameScratch.Core.Common.Responses;

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
    public Dictionary<string, Func<IActionContext, ActionResponse>> Actions { get; init; }

    public ActionService(IDiceService diceService)
    {
        _diceService = diceService;

        Actions = new()
        {
            { nameof(Attack), ctx => Attack(ctx.Attacker, ctx.Defender) },
            { nameof(Guard), ctx => Guard(ctx.Attacker) },
            { nameof(EndTurn), ctx => EndTurn(ctx.Attacker) }
        };
    }

    public ActionResponse Attack(Player attacker, Player defender)
    {
        if (attacker.GetStaminaPointsRemaining() <= 0)
            return Exhausted(attacker, nameof(Attack));

        var sb = new StringBuilder();
        sb.Append($"{attacker.Profile.Name} ATTACKS {defender.Profile.Name} with {attacker.Equipment.Weapon.Name}");

        int attackRoll = _diceService.Roll(DiceType.D20);

        AttackResponse attackResponse = new()
        {
            DefenderArmorClass = defender.Stats.ArmorClass,
            DefenderArmorClassModifiers = defender.GetArmorClassModifiers(),
            DefenderTotalArmorClass = defender.GetTotalArmorClass(),
            AttackerDiceRoll = attackRoll,
            AttackerDiceRollModifiers = attacker.GetAttackDiceRollModifiers(),
            AttackerTotalDiceRoll = attackRoll + attacker.GetAttackDiceRollModifiers().Sum()
        };

        int defenderTotalArmorClass = defender.GetTotalArmorClass();
        bool attackSuccessfull = attackResponse.AttackerTotalDiceRoll >= attackResponse.DefenderTotalArmorClass;

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

        attacker.UseWeapon();

        return new ActionResponse()
        {
            Message = sb.ToString(),
            Attack = attackResponse,
            SwitchPlayerTurn = attacker.GetStaminaPointsRemaining() <= 0
        };
    }

    public ActionResponse Guard(Player attacker)
    {
        if (attacker.GetStaminaPointsRemaining() <= 0)
            return Exhausted(attacker, nameof(Guard));

        attacker.Conditions.StanceType = StanceType.Guard;
        return new ActionResponse() 
        {
            Message = $"{attacker.Profile.Name} goes into GUARD STANCE",
            SwitchPlayerTurn = true
        };
    }

    public ActionResponse EndTurn(Player attacker)
    {
        return new()
        {
            Message = $"{attacker.Profile.Name} ends turn.",
            SwitchPlayerTurn = true
        };
    }

    public ActionResponse InvokeAction(string actionName, Player attacker, Player defender)
    {
        if (!Actions.TryGetValue(actionName, out var action))
            throw new ArgumentException($"Unknown action: {actionName}", nameof(actionName));

        IActionContext context = new ActionContext(attacker, defender);

        return action(context);
    }

    public ActionResponse RollIniative(Player player)
    {
        int result = _diceService.Roll(DiceType.D20) + player.GetInitiativeModifier();

        return new()
        {
            Message = $"{player.Profile.Name} rolls {result}",
            RollInitiative = new() { DiceRoll = result }
        };
    }

    public ActionResponse Exhausted(Player player, string action)
    {
        StringBuilder sb = new();
        sb.AppendLine($"{player.Profile.Name} tried to {action} but is tired due to having {player.GetStaminaPointsRemaining()} StaminaPoints remaining");
        sb.AppendLine($"{player.Profile.Name} is now in {StanceType.Exhausted} stance");
        player.Conditions.StanceType = StanceType.Exhausted;

        return new ActionResponse()
        {
            Message = sb.ToString(),
            SwitchPlayerTurn = player.GetStaminaPointsRemaining() <= 0
        };
    }
}