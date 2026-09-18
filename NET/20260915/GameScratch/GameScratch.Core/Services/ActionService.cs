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
            { nameof(Guard), ctx => Guard(ctx.Attacker, ctx.Defender) },
            { nameof(Wait), ctx => Wait(ctx.Attacker, ctx.Defender) }
        };
    }

    public ActionResponse Attack(Player attacker, Player defender)
    {
        var sb = new StringBuilder();

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
            string unbalancedMsg = string.Empty;
            if (defender.Conditions.StanceType == StanceType.Unbalanced)
                unbalancedMsg = $" {defender.Profile.Name} remains off balance.";

            sb.Append($" and hits, doing {damage} HitPoints damage and {attacker.Equipment.Weapon.StaminaPointsDamage} StaminaPoints damage.");

            // check if defender is staggered
            int pushRoll = _diceService.Roll(DiceType.D20);
            int attackerTotalPushRoll = pushRoll + attacker.GetPushDiceRollModifiers().Sum();
            int attackerPushModifier = damage;
            int defenderTotalPoiseClass = defender.GetTotalPoiseClass(attackerPushModifier);

            bool pushSuccessfull = attackerTotalPushRoll >= defenderTotalPoiseClass;

            if (pushSuccessfull)
            {
                attackResponse.Push = new()
                {
                    DefenderPoiseClass = defender.Stats.PoiseClass,
                    DefenderPoiseClassModifiers = defender.GetPoiseClassModifiers(),
                    DefenderTotalPoiseClass = defenderTotalPoiseClass,
                    AttackerPushModifier = attackerPushModifier,
                    AttackerDiceRoll = pushRoll,
                    AttackerDiceRollModifiers = attacker.GetPushDiceRollModifiers(),
                    AttackerTotalDiceRoll = attackerTotalPushRoll 
                };

                if (defender.Conditions.StanceType == StanceType.Staggered)
                {
                    sb.Append($" {defender.Profile.Name} still reeling!");
                }
                else
                {
                    defender.Conditions.StanceType = StanceType.Staggered;
                    sb.Append($" {defender.Profile.Name} is visibly shaken from that attack!");
                }                
            }
            else
            {
                sb.Append(unbalancedMsg);
            }
        }
        else
        {
            // check if attacker is unbalanced
            int counterRoll = _diceService.Roll(DiceType.D20);
            int defenderTotalCounterRoll = counterRoll + defender.GetCounterDiceRollModifiers().Sum();
            int attackerMissModifier = attackResponse.DefenderTotalArmorClass - int.Max(0, attackResponse.AttackerTotalDiceRoll);
            int attackerTotalBalanceClass = attacker.GetTotalBalanceClass(attackerMissModifier);

            bool counterSuccessfull = defenderTotalCounterRoll >= attackerTotalBalanceClass;

            if (counterSuccessfull)
            {
                attackResponse.Counter = new()
                {
                    AttackerBalanceClass = attacker.Stats.BalanceClass,
                    AttackerBalanceClassModifiers = attacker.GetBalanceClassModifiers(),
                    AttackerTotalBalanceClass = attackerTotalBalanceClass,
                    AttackerMissModifier = attackerMissModifier,
                    DefenderDiceRoll = counterRoll,
                    DefenderDiceRollModifiers = defender.GetCounterDiceRollModifiers(),
                    DefenderTotalDiceRoll = defenderTotalCounterRoll 
                };

                attacker.Conditions.StanceType = StanceType.Unbalanced;
                sb.Append($" but missed and lost his footing!");
            }
            else
            {
                sb.Append($" but missed!");
            }

            if (defender.Conditions.StanceType == StanceType.Unbalanced || defender.Conditions.StanceType == StanceType.Staggered)
            {
                defender.Conditions.StanceType = StanceType.Neutral;
                sb.AppendLine($" {defender.Profile.Name} regains composure.");
            }
                
        }

        attacker.UseWeapon();

        bool switchPlayerTurn = attacker.IsExhausted() || attacker.Conditions.StanceType == StanceType.Unbalanced;

        if (switchPlayerTurn)
        {
            
            sb.AppendLine();
            if (attacker.Conditions.StanceType != StanceType.Unbalanced && attacker.IsExhausted())
                sb.AppendLine($"{attacker.Profile.Name} pushed too hard and runs out of steam.");

            string recoverMsg = string.Empty;
            if (defender.Conditions.StanceType == StanceType.Staggered)
                recoverMsg = "steadies himself and ";

            if (defender.Conditions.StanceType != StanceType.Guard )
                defender.Conditions.StanceType = defender.Conditions.StanceType = StanceType.Neutral;
                
            sb.AppendLine($"{defender.Profile.Name} {recoverMsg}takes the initiative.");
        }

        return new ActionResponse()
        {
            Message = sb.ToString(),
            Attack = attackResponse,
            SwitchPlayerTurn = switchPlayerTurn
        };
    }

    public ActionResponse Guard(Player attacker, Player defender)
    {
        StringBuilder sb = new();
        sb.AppendLine($"{attacker.GetNameWithStatus()} braces and goes into GUARD STANCE.");
        
        attacker.Conditions.StanceType = StanceType.Guard;

        string recoverMsg = string.Empty;
            if (defender.Conditions.StanceType == StanceType.Staggered || defender.Conditions.StanceType == StanceType.Unbalanced)
                recoverMsg = "steadies himself and ";
        sb.AppendLine($"{defender.Profile.Name} {recoverMsg}takes the initiative.");

        if (defender.Conditions.StanceType != StanceType.Guard )
            defender.Conditions.StanceType = defender.Conditions.StanceType = StanceType.Neutral;

        return new ActionResponse() 
        {
            Message = sb.ToString(),
            SwitchPlayerTurn = true
        };
    }

    public ActionResponse Wait(Player attacker, Player defender)
    {
        StringBuilder sb = new();
        sb.AppendLine($"{attacker.GetNameWithStatus()} waits and goes into NEUTRAL STANCE.");
        
        attacker.Conditions.StanceType = StanceType.Neutral;

        string recoverMsg = string.Empty;
            if (defender.Conditions.StanceType == StanceType.Staggered || defender.Conditions.StanceType == StanceType.Unbalanced)
                recoverMsg = "steadies himself and ";
        sb.AppendLine($"{defender.Profile.Name} {recoverMsg}takes the initiative.");

        if (defender.Conditions.StanceType != StanceType.Guard )
            defender.Conditions.StanceType = defender.Conditions.StanceType = StanceType.Neutral;
        
        return new()
        {
            Message = sb.ToString(),
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
}