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
            { nameof(Wait), ctx => Wait(ctx.Attacker) }
        };
    }

    public ActionResponse Attack(Player attacker, Player defender)
    {
        var sb = new StringBuilder();

        attacker.UseWeapon();

        sb.Append($"{attacker.GetNameWithStatus()} ATTACKS {defender.GetNameWithStatus()} with {attacker.Equipment.Weapon.Name}");

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

        bool attackSuccessfull = attackResponse.AttackerTotalDiceRoll >= attackResponse.DefenderTotalArmorClass;

        if (attackSuccessfull)
        {
            int damage = _diceService.Roll(attacker.Equipment.Weapon.HitPointsDamageDiceType);
            defender.AddHitPointsDamage(damage);
            defender.AddStaminaPointsDamage(attacker.Equipment.Weapon.StaminaPointsDamage);
            sb.Append($" and hits, doing {damage} HitPoints damage and {attacker.Equipment.Weapon.StaminaPointsDamage} StaminaPoints damage ");
        }
        else
        {
            // check if attacker is unbalanced
            int counterRoll = _diceService.Roll(DiceType.D20);
            int defenderTotalCounterRoll = counterRoll + defender.GetCounterDiceRollModifiers().Sum();
            int attackerMissModifier = attackResponse.DefenderTotalArmorClass - int.Max(0, attackResponse.AttackerTotalDiceRoll);

            bool counterSuccessfull = defenderTotalCounterRoll >= attacker.GetTotalBalanceClass() - attackerMissModifier;

            if (counterSuccessfull)
            {
                attackResponse.Counter = new()
                {
                    AttackerBalanceClass = attacker.Stats.BalanceClass,
                    AttackerBalanceClassModifiers = attacker.GetBalanceClassModifiers(),
                    AttackerTotalBalanceClass = attacker.GetTotalBalanceClass() - attackerMissModifier,
                    AttackerMissModifier = attackerMissModifier,
                    DefenderDiceRoll = counterRoll,
                    DefenderDiceRollModifiers = defender.GetCounterDiceRollModifiers(),
                    DefenderTotalDiceRoll = defenderTotalCounterRoll 
                };

                attacker.Conditions.StanceType = StanceType.Unbalanced;
                sb.Append($" but missed and lost balance!");
            }
            else
            {
                sb.Append($" but missed!");
            }

            if (defender.Conditions.StanceType == StanceType.Unbalanced || defender.Conditions.StanceType == StanceType.Staggered)
                defender.Conditions.StanceType = StanceType.Neutral;

        }

        return new ActionResponse()
        {
            Message = sb.ToString(),
            Attack = attackResponse,
            SwitchPlayerTurn = attacker.IsExhausted() || attacker.Conditions.StanceType == StanceType.Unbalanced
        };
    }

    public ActionResponse Guard(Player attacker)
    {
        attacker.Conditions.StanceType = StanceType.Guard;
        return new ActionResponse() 
        {
            Message = $"{attacker.GetNameWithStatus()} goes into GUARD STANCE",
            SwitchPlayerTurn = true
        };
    }

    public ActionResponse Wait(Player attacker)
    {
        attacker.Conditions.StanceType = StanceType.Neutral;
        return new()
        {
            Message = $"{attacker.GetNameWithStatus()} goes into NEUTRAL STANCE",
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

    // public ActionResponse Exhausted(Player player, string action)
    // {
    //     StringBuilder sb = new();
    //     sb.AppendLine($"{player.Profile.Name} tried to {action} but is exhausted due to having {player.GetStaminaPointsRemaining()} StaminaPoints remaining");

    //     return new ActionResponse()
    //     {
    //         Message = sb.ToString(),
    //         SwitchPlayerTurn = player.IsExhausted()
    //     };
    // }
}