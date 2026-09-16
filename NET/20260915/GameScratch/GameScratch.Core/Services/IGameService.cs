using GameScratch.Core.Common.Player;

namespace GameScratch.Core.Services;

public interface IGameService
{
    Player Challenger { get; set; }
    Task<string> SendMessageToLLMAsync(string message);

    void Reset();
}