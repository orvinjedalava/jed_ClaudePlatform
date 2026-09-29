using GameScratch.Contracts.Enums;

namespace GameScratch.Contracts.Entities.Responses;

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

}