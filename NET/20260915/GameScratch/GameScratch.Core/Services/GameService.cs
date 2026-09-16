using GameScratch.Core.Common;
using GameScratch.Core.Common.Players;
using GameScratch.Core.Common.Weapons;
using GameScratch.Core.LLM;

namespace GameScratch.Core.Services;

public class GameService: IGameService
{
    private readonly ILLMService _llmService;
    private readonly IPlayerService _playerService;

    private readonly Dictionary<GameState, string> _promptsMap;

    public GameService(
        ILLMService llmService,
        IPlayerService playerService)
    {
        _llmService = llmService ?? throw new ArgumentNullException("LLMService not injected.");
        _playerService = playerService ?? throw new ArgumentNullException("PlayerService not injected.");

        LatestGameState = GameState.None;

        _promptsMap = PromptFactory.Create().Build();
    }

    public Player Challenger { get; set; } = null!;
    public Player Champion { get; set; } = null!;
    public GameState LatestGameState { get; set; }

    async Task<string> IGameService.SendMessageToLLMAsync(string message)
    {
        return await _llmService.SendMessageAsync(message);
    }

    string IGameService.Reset()
    {
        _llmService.ClearChatHistory();
        _playerService.ResetPlayer(Challenger);
        _playerService.ResetPlayer(Champion);

        LatestGameState = GameState.ChallengerTurn;

        return _promptsMap[LatestGameState];
    }

    string IGameService.Start()
    {
        _llmService.ClearChatHistory();

        Challenger = _playerService.CreatePlayer(
            RoleType.Challenger,
            "Player",
            WeaponType.BareHands
        );

        Champion = _playerService.CreatePlayer(
            RoleType.Champion,
            "Champion",
            WeaponType.BareHands
        );

        LatestGameState = GameState.ChallengerTurn;

        return _promptsMap[LatestGameState];
    }
}