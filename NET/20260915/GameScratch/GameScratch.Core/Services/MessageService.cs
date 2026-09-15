using GameScratch.Core.LLM;

namespace GameScratch.Core.Services;

public class MessageService(ILLMService llmService) : IMessageService
{
    private readonly ILLMService _llmService = llmService ?? throw new ArgumentNullException("LLMService not injected.");

    async Task<string> IMessageService.SendMessageToLLMAsync(string message)
    {
        return await _llmService.SendMessageAsync(message);
    }
}