using System.Text;
using GameScratch.Contracts.Entities.Responses;
using GameScratch.Contracts.Enums;

namespace GameScratch.Core.Extensions;

public static class PlayersResponseExtensions
{
    public static string ToConsoleString(this PlayersResponse response, GameTurn gameState)
    {
        if (response.Challenger == null && response.Champion == null)
            return string.Empty;
        
        StringBuilder sb = new();

        sb.AppendLine("---------------------------------");
        sb.AppendLine("Gladiatiors Current Stats:");

        if (gameState == GameTurn.ChallengerTurn)
        {
            if (response.Champion != null)
            {
                sb.AppendLine();
                sb.AppendLine(response.Champion.ToConsoleString());
            }
            if (response.Challenger != null)
            {
                sb.AppendLine();
                sb.AppendLine(response.Challenger.ToConsoleString());
            }     
        }
        else if (gameState == GameTurn.ChampionTurn)
        {
            if (response.Challenger != null)
            {
                sb.AppendLine();
                sb.AppendLine(response.Challenger.ToConsoleString());
            }
            if (response.Champion != null)
            {
                sb.AppendLine();
                sb.AppendLine(response.Champion.ToConsoleString());
            }
        }
        
        return sb.ToString();
    }
}