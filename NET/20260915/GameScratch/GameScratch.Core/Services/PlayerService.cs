using GameScratch.Core.Common.Players;
using GameScratch.Core.Common.Weapons;
using GameScratch.Core.Common.Responses;
using System.Text;

namespace GameScratch.Core.Services;

public class PlayerService : IPlayerService
{
    private IActionService _actionService;
    public PlayerService(IActionService actionService)
    {
        _actionService = actionService;
    }

    public void ResetPlayer(Player player)
    {
        player.ClearConditions();
    }

    public Player CreatePlayer(RoleType roleType, string name, WeaponType weaponType)
    {
        Player player = PlayersFactory.DefaultPlayer;

        Weapon weapon = WeaponBuilder
            .Create()
            .FromWeaponType(weaponType)
            .Build();

        player.Equipment.Weapon = weapon;
        player.Profile.Name = name;
        player.Profile.RoleType = roleType;

        return player;
    }

    public ActionResponse StartPlayerTurn(Player player)
    {
        player.StartTurn();

        return new();
    }

    public ActionResponse Attack(Player attacker, Player defender)
    {
        return _actionService.Attack(attacker, defender);
    }

    public ActionResponse Guard(Player player)
    {
        return _actionService.Guard(player);
    }

    public ActionResponse EndTurn(Player player)
    {
        return _actionService.Wait(player);
    }

    public ActionResponse InvokeRandomAction(Player attacker, Player defender)
    {
        var actionNames = _actionService.Actions.Keys.ToList();
        
        string chosenAction = actionNames[Random.Shared.Next(actionNames.Count)];

        IActionContext context = new ActionContext(attacker, defender);

        return _actionService.Actions[chosenAction](context);
    }

    public (ActionResponse, Player) RollIniative(Player challenger, Player champion)
    {
        ActionResponse challengerRoll = _actionService.RollIniative(challenger);
        ActionResponse championRoll = _actionService.RollIniative(champion);

        var sb = new StringBuilder();

        sb.AppendLine("Initiative Roll:");
        sb.AppendLine();
        sb.AppendLine($"{challengerRoll.Message}");
        sb.AppendLine($"{championRoll.Message}");
        sb.AppendLine();
        Player winningPlayer;

        if (championRoll.RollInitiative?.DiceRoll >= challengerRoll.RollInitiative?.DiceRoll)
        {
            sb.AppendLine($"{champion.Profile.Name} goes first.");
            winningPlayer = champion;
        }
        else
        {
            sb.AppendLine($"{challenger.Profile.Name} goes first.");
            winningPlayer = challenger;
        }

        return (new ActionResponse() { Message = sb.ToString() }, winningPlayer );
    }
}