using System.Text;
using GameScratch.Core.Common.Players;

namespace GameScratch.Core.Common.Responses;

public class GameResponse
{
    public required bool ContinueState { get; init; }
    public required GameState GameState { get; init; }

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
        if (Players != null)
            sb.AppendLine(Players.ToConsoleString());
        if (GameState == GameState.ChallengerTurn || GameState == GameState.ChampionTurn)
        {

            sb.AppendLine("---------------------------------");
            sb.AppendLine($"It's your turn, {Players?.GetCurrentPlayerTurnName(GameState)}.");
        }
        if (PlayerOptions != null)
            sb.AppendLine(PlayerOptions.ToConsoleString());

        return sb.ToString();
    }

}