
using System.Text;
using GameScratch.Core.Common.Players;
using GameScratch.Contracts.Enums;

namespace GameScratch.Core.Common.Responses;

public class PlayersResponse
{
    public Player? Challenger { get; init; }
    public Player? Champion { get; init; }

    public string GetCurrentPlayerTurnName(GameTurn gameState)
    {
        switch(gameState)
        {
            case GameTurn.ChallengerTurn:
                return Challenger?.Profile.Name ?? string.Empty;
            case GameTurn.ChampionTurn:
                return Champion?.Profile.Name ?? string.Empty;
            default:
                throw new NotImplementedException();
        }
    }

    public string ToConsoleString(GameTurn gameState)
    {
        if (Challenger == null && Champion == null)
            return string.Empty;
        
        StringBuilder sb = new();

        sb.AppendLine("---------------------------------");
        sb.AppendLine("Gladiatiors Current Stats:");

        if (gameState == GameTurn.ChallengerTurn)
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
        else if (gameState == GameTurn.ChampionTurn)
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