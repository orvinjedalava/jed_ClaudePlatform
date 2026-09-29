using GameScratch.Contracts.Entities.Responses;
using GameScratch.Contracts.Enums;
using GameScratch.Contracts.Entities.Players;

namespace GameScratch.Core.Services;

public interface IGameService
{
    Player Challenger { get; set; }
    Player Champion { get; set; }
    GameTurn LatestGameTurn { get; set; }

    GameResponse ShowMainMenu();
    string Reset();
    GameResponse StartMatch(bool continueState);
    GameResponse StartPlayerTurn(bool continueState, string message = "");
    GameResponse CloseGame(bool continueState);
    GameResponse Surrender(bool continueState);
    GameResponse HandleInput(char keyChar);
    Task<GameResponse> ExecuteChampionTurnAsync();

    Task<GameResponse> GetChampionActionChoiceAsync();
    Task<GameResponse> GetChampionActionResultAsync();

    Player GetAttackingPlayer();
    Player GetDefendingPlayer();

    void SwitchPlayerTurn();
}