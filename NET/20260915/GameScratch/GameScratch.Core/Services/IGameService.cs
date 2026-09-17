using GameScratch.Core.Common;
using GameScratch.Core.Common.Players;
using GameScratch.Core.Common.Responses;

namespace GameScratch.Core.Services;

public interface IGameService
{
    Player Challenger { get; set; }
    Player Champion { get; set; }
    GameState LatestGameState { get; set; }

    Task<string> SendMessageToLLMAsync(string message);

    GameResponse ShowMainMenu();
    string Reset();
    GameResponse StartMatch(bool continueState);
    GameResponse StartPlayerTurn(bool continueState, string message = "");
    GameResponse CloseGame(bool continueState);
    GameResponse Surrender(bool continueState);
    GameResponse HandleInput(char keyChar);
    Task<GameResponse> ExecuteChampionTurnAsync();

    Player GetAttackingPlayer();
    Player GetDefendingPlayer();

    void SwitchPlayerTurn();
}