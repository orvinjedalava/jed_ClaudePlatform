using GameScratch.Core.Common.Players;

namespace GameScratch.Core.Common.Responses;

public class GameResponse
{
    public required bool ContinueState { get; init; }
    public Player? Challenger { get; init; }
    public Player? Champion { get; init; }
    public required GameState GameState { get; init; }

    public string Message { get; set; } = string.Empty;

    public ActionResponse? Action { get; set; }
    public StartTurnResponse? StartTurn { get; set; }
    public PlayerOptionsResponse? PlayerOptions { get; set; }

}