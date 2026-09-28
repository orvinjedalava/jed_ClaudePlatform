using GameScratch.Contracts.DTOs;

namespace GameScratch.Contracts.DTOs;

public record GameResponseDto
{
    public required bool ContinueState { get; init; }
    public required string GameTurn { get; init; }
    public required string GameMode { get; init; }

    public string Message { get; set; } = string.Empty;

    public List<string> History { get; set; } = null!;
    public PlayersResponseDto? Players { get; set; }
    public ActionResponseDto? Action { get; set; }
    public StartTurnResponseDto? StartTurn { get; set; }
    public PlayerOptionsResponseDto? PlayerOptions { get; set; }
}