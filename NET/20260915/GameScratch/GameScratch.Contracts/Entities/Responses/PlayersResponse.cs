
using GameScratch.Contracts.Entities.Players;
using GameScratch.Contracts.Enums;

namespace GameScratch.Contracts.Entities.Responses;

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

    
}