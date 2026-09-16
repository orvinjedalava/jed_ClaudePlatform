using GameScratch.Core.Common.Players;

namespace GameScratch.Core.Common;

public class GameResponse
{
    public required Player Challenger { get; set; }
    public required Player Champion { get; set; }

    public string Message { get; set; } = string.Empty;

}