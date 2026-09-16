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
    public Dictionary<string, Func<IActionContext, string>> Actions { get; init; }

    public ActionService()
    {
        Actions = new()
        {
            { nameof(Attack), ctx => Attack(ctx.Attacker, ctx.Defender) },
            { nameof(GuardStance), ctx => GuardStance(ctx.Attacker) }
        };
    }

    public string Attack(Player attacker, Player defender)
    {
        return $"{attacker.Profile.Name} ATTACKS {defender.Profile.Name} with {attacker.Equipment.Weapon.Name}";
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