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
    }

    public async Task<char> ChooseActionAsync(GameResponse gameResponse)
    {
        // simulate model thinking.
        await Task.Delay(5000);

        var choices = gameResponse.PlayerOptions!.Options
            .Where(o => o.Key != 'q')
            .ToList();

        var random = new Random();
        return choices[random.Next(choices.Count)].Key;
    }

    public async Task<(char, string)> SendMessageAsync(GameResponse gameResponse)
    {
        throw new NotImplementedException();
    }

    public async Task<(char, string)> GetToolChoiceAsync(GameResponse gameResponse)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetToolChoiceResponseAsync(GameResponse gameResponse)
    {
        throw new NotImplementedException();
    }
}