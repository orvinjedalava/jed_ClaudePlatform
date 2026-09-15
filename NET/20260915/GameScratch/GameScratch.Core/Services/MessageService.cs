using GameScratch.Core.LLM;

namespace GameScratch.Core.Services;

public class MessageService(ILLMService llmService) : IMessageService
{
    private readonly ILLMService _llmService = llmService ?? throw new ArgumentNullException("LLMService not injected.");

    string IMessageService.SendMessageToLLM(string message)
    {
        return _llmService.SendMessage(message);
    }
}