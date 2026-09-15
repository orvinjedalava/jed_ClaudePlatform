namespace GameScratch.Core.Services;

public interface IGameService
{
    Task<string> SendMessageToLLMAsync(string message);

    void Reset();
}