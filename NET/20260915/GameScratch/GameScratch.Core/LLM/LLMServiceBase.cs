using GameScratch.Core.Common.Players;
using GameScratch.Core.Common.Responses;
using GameScratch.Core.Services;

namespace GameScratch.Core.LLM;

public class LLMServiceBase : ILLMService
{
    protected readonly IPlayerService _playerService;
    public LLMServiceBase(IPlayerService playerService)
    {
        _playerService = playerService ?? throw new ArgumentNullException("PlayerService not dependency injected.");

        ChatHistory = [];
    }

    public List<Chat> ChatHistory { get; init; }

    void ILLMService.ClearChatHistory()
    {
        ChatHistory.Clear();
    }

    public async Task<char> ChooseActionAsync(GameResponse gameResponse)
    {
        var choices = gameResponse.PlayerOptions!.Options
            .Where(o => o.Key != 'q')
            .ToList();

        var random = new Random();
        return choices[random.Next(choices.Count)].Key;
    }

    public Task<string> SendMessageAsync(string message)
    {
        throw new NotImplementedException();
    }
}