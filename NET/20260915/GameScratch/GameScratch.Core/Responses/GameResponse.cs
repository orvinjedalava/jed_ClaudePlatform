using System.Text;
using GameScratch.Contracts.Enums;

namespace GameScratch.Core.Responses;

public class GameResponse
{
    public required bool ContinueState { get; init; }
    public required GameTurn GameTurn { get; init; }
    public required GameMode GameMode { get; init; }

    public string Message { get; set; } = string.Empty;

    public List<string> History { get; set; } = null!;
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

        if (History.Count > 0)
        {
            sb.AppendLine("---------------------------------");
            sb.AppendLine("History:");
            sb.AppendLine(string.Join(Environment.NewLine, History));
        }
       
            
        if (Players != null && Players.Challenger != null && Players.Champion != null)
            sb.AppendLine(Players.ToConsoleString(GameTurn));
        if (GameTurn == GameTurn.ChallengerTurn || GameTurn == GameTurn.ChampionTurn)
        {

            sb.AppendLine("---------------------------------");
            sb.AppendLine($"It's your turn, {Players?.GetCurrentPlayerTurnName(GameTurn)}.");
        }
        if (PlayerOptions != null && (GameTurn == GameTurn.ChallengerTurn || GameMode == GameMode.TwoPlayers || GameTurn == GameTurn.None))
        {
            sb.AppendLine(PlayerOptions.ToConsoleString());
                
        }

        return sb.ToString();
    }

}