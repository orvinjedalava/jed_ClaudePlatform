using System.Text;
using GameScrach.Core.Common;
using GameScratch.Core.Common.Players;

namespace GameScratch.Core.Common.Responses;

public class GameResponse
{
    public required bool ContinueState { get; init; }
    public required GameState GameState { get; init; }
    public required GameMode GameMode { get; init; }

    public string Message { get; set; } = string.Empty;

    public PlayersResponse? Players { get; set; }
    public ActionResponse? Action { get; set; }
    public StartTurnResponse? StartTurn { get; set; }
    public PlayerOptionsResponse? PlayerOptions { get; set; }

    public string ToConsoleString()
    {
        StringBuilder sb = new();

        sb.AppendLine("---------------------------------");
        sb.AppendLine(Message);
        sb.AppendLine();
        if (Action != null)
            sb.AppendLine(Action.ToConsoleString());
        if (Players != null && Players.Challenger != null && Players.Champion != null)
            sb.AppendLine(Players.ToConsoleString(GameState));
        if (GameState == GameState.ChallengerTurn || GameState == GameState.ChampionTurn)
        {

            sb.AppendLine("---------------------------------");
            sb.AppendLine($"It's your turn, {Players?.GetCurrentPlayerTurnName(GameState)}.");
        }
        if (PlayerOptions != null)
        {
            if (GameState != GameState.ChampionTurn || GameMode == GameMode.TwoPlayers)
                sb.AppendLine(PlayerOptions.ToConsoleString());
            else
                sb.AppendLine($"{Players!.Champion!.Profile.Name} is thinking...");
        }

        return sb.ToString();
    }

}