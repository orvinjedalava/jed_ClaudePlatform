using GameScratch.Core.Common.Players;

namespace GameScratch.Core.LLM;

public interface ILLMService
{
    List<Chat> ChatHistory { get; init; }
    void ClearChatHistory();
    Task<string> ExecuteTurnAsync(Player champion, Player challenger);
    Task<string> SendMessageAsync(string message);
}