namespace GameScratch.Core.LLM;

public interface ILLMService
{
    List<Chat> ChatHistory { get; init; }
    void ClearChatHistory();
    Task<string> SendMessageAsync(string message);
}