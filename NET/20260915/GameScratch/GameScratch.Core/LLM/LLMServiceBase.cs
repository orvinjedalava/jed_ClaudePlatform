using GameScratch.Core.Services;

namespace GameScratch.Core.LLM;

public class LLMServiceBase : ILLMService
{
    private readonly IActionService _actionService;
    public LLMServiceBase(IActionService actionService)
    {
        _actionService = actionService ?? throw new ArgumentNullException("ActionService not dependency injected.");
        
        ChatHistory = [];
    }

    public List<Chat> ChatHistory { get; init; }

    void ILLMService.ClearChatHistory()
    {
        ChatHistory.Clear();
    }

    Task<string> ILLMService.SendMessageAsync(string message)
    {
        throw new NotImplementedException();
    }
}