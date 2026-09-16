using GameScratch.Core.Common.Players;
using GameScratch.Core.Services;

namespace GameScratch.Core.LLM;

public class LLMServiceBase : ILLMService
{
    protected readonly IActionService _actionService;
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

    public async Task<string> ExecuteTurnAsync(Player champion, Player challenger)
    {
        var actionNames = _actionService.Actions.Keys.ToList();
        string chosenAction = actionNames[Random.Shared.Next(actionNames.Count)];

        IActionContext context = new ActionContext(champion, challenger);

        return _actionService.Actions[chosenAction](context);
    }

    public Task<string> SendMessageAsync(string message)
    {
        throw new NotImplementedException();
    }
}