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
                return Challenger?.Profile.Name ?? string.Empty;
            case GameState.ChampionTurn:
                return Champion?.Profile.Name ?? string.Empty;
            default:
                throw new NotImplementedException();
        }
    }

    public string ToConsoleString(GameState gameState)
    {
        if (Challenger == null && Champion == null)
            return string.Empty;
        
        StringBuilder sb = new();

        sb.AppendLine("---------------------------------");
        sb.AppendLine("Gladiatiors Current Stats:");

        if (gameState == GameState.ChallengerTurn)
        {
            if (Champion != null)
            {
                sb.AppendLine();
                sb.AppendLine(Champion.ToConsoleString());
            }
            if (Challenger != null)
            {
                sb.AppendLine();
                sb.AppendLine(Challenger.ToConsoleString());
            }     
        }
        else if (gameState == GameState.ChampionTurn)
        {
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
        }
        
        return sb.ToString();
    }
}