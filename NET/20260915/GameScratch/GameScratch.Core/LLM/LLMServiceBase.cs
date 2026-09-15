namespace GameScratch.Core.LLM;

public class LLMServiceBase : ILLMService
{
    public LLMServiceBase()
    {
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