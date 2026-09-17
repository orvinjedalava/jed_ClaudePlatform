using System.Reflection.Metadata.Ecma335;
using System.Text;
using GameScratch.Core.Common.Players;

namespace GameScratch.Core.Common.Responses;

public class PlayersResponse
{
    public Player? Challenger { get; init; }
    public Player? Champion { get; init; }

    public string GetCurrentPlayerTurnName(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.ChallengerTurn:
                return Challenger?.GetNameWithStatus() ?? string.Empty;
            case GameState.ChampionTurn:
                return Champion?.GetNameWithStatus() ?? string.Empty;
            default:
                throw new NotImplementedException();
        }
    }

    public string ToConsoleString()
    {
        if (Challenger == null && Champion == null)
            return string.Empty;
        
        StringBuilder sb = new();

        sb.AppendLine("---------------------------------");
        sb.AppendLine("Gladiatiors Current Stats:");
        if (Challenger != null)
        {
            sb.AppendLine();
            sb.AppendLine(Challenger.ToConsoleString());
        }
        if (Champion != null)
        {
            sb.AppendLine();
            sb.AppendLine(Champion.ToConsoleString());
        }
        return sb.ToString();
    }
}