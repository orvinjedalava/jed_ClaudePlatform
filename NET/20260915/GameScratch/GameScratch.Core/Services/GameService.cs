using GameScratch.Core.LLM;

namespace GameScratch.Core.Services;

public class GameService(ILLMService llmService) : IGameService
{
    private readonly ILLMService _llmService = llmService ?? throw new ArgumentNullException("LLMService not injected.");

    async Task<string> IGameService.SendMessageToLLMAsync(string message)
    {
        return await _llmService.SendMessageAsync(message);
    }

    void IGameService.Reset()
    {
        _llmService.ClearChatHistory();
    }
}