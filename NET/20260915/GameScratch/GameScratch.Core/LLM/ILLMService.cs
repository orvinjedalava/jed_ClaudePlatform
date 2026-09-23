using GameScratch.Core.Common.Players;
using GameScratch.Core.Common.Responses;

namespace GameScratch.Core.LLM;

public interface ILLMService
{
    Task<char> ChooseActionAsync(GameResponse gameResponse);
    Task<(char, string)> SendMessageAsync(GameResponse gameResponse);

    Task<(char, string)> GetToolChoiceAsync(GameResponse gameResponse);
}