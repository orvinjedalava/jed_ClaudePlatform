using GameScratch.Core.Common.Players;
using GameScratch.Core.Common.Responses;

namespace GameScratch.Core.LLM;

public interface ILLMService
{
    List<Chat> ChatHistory { get; init; }
    void ClearChatHistory();
    Task<char> ChooseActionAsync(GameResponse gameResponse);
    Task<string> SendMessageAsync(string message);
}