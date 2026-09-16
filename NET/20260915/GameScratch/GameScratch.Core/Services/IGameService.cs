using GameScratch.Core.Common.Players;

namespace GameScratch.Core.Services;

public interface IGameService
{
    Player Challenger { get; set; }
    Player Champion { get; set; }
    Task<string> SendMessageToLLMAsync(string message);

    void Reset();
}