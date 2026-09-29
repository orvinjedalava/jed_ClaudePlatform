using System.Text;
using GameScratch.Contracts.Entities.Responses;
using GameScratch.Contracts.Enums;

namespace GameScratch.Core.Extensions;

public static class GameResponseExtensions
{
    public static string ToConsoleString(this GameResponse response)
    {
        StringBuilder sb = new();

        sb.AppendLine("---------------------------------");
        sb.AppendLine(response.Message);
        sb.AppendLine();
        if (response.Action != null)
            sb.AppendLine(response.Action.ToConsoleString());

        if (response.History.Count > 0)
        {
            sb.AppendLine("---------------------------------");
            sb.AppendLine("History:");
            sb.AppendLine(string.Join(Environment.NewLine, response.History));
        }
       
            
        if (response.Players != null && response.Players.Challenger != null && response.Players.Champion != null)
            sb.AppendLine(response.Players.ToConsoleString(response.GameTurn));
        if (response.GameTurn == GameTurn.ChallengerTurn || response.GameTurn == GameTurn.ChampionTurn)
        {

            sb.AppendLine("---------------------------------");
            sb.AppendLine($"It's your turn, {response.Players?.GetCurrentPlayerTurnName(response.GameTurn)}.");
        }
        if (response.PlayerOptions != null && (response.GameTurn == GameTurn.ChallengerTurn || response.GameMode == GameMode.TwoPlayers || response.GameTurn == GameTurn.None))
        {
            sb.AppendLine(response.PlayerOptions.ToConsoleString());
                
        }

        return sb.ToString();
    }
}