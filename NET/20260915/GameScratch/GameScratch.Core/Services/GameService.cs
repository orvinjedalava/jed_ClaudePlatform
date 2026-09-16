using GameScratch.Core.Common;
using GameScratch.Core.Common.Players;
using GameScratch.Core.Common.Weapons;
using GameScratch.Core.LLM;

namespace GameScratch.Core.Services;

public class GameService: IGameService
{
    private readonly ILLMService _llmService;
    private readonly IPlayerService _playerService;

    private readonly Dictionary<string, string> _promptsMap;

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

    public string ShowMainMenu()
    {
        LatestGameState = GameState.None;

        return _promptsMap[LatestGameState.ToString()];
    }

    public string Reset()
    {
        _llmService.ClearChatHistory();
        _playerService.ResetPlayer(Challenger);
        _playerService.ResetPlayer(Champion);

        LatestGameState = GameState.ChallengerTurn;

        return _promptsMap[LatestGameState.ToString()];
    }

    public string Start()
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

        return _promptsMap[LatestGameState.ToString()];
    }

    public (bool continueGame, string responseMsg) HandleInput(char keyChar)
    {
        switch(LatestGameState)
        {
            case GameState.ChallengerTurn:
                return HandleChallengerInput(keyChar);
            case GameState.None:
                return HandleGameStateNoneInput(keyChar);
        }

        return (false, string.Empty);
    }

    public (bool continueGame, string responseMsg) HandleChallengerInput(char keyChar)
    {
        bool isContinue = false;
        string responseMsg = string.Empty;

        switch(keyChar)
        {
            case '1':
                LatestGameState = GameState.ChampionTurn;
                isContinue = true;
                responseMsg = _promptsMap[LatestGameState.ToString()];
                break;
            case '2':
                LatestGameState = GameState.ChampionTurn;
                isContinue = true;
                responseMsg = _promptsMap[LatestGameState.ToString()];
                break;
            case 'r':
                isContinue = true;
                responseMsg = Reset();
                break;
            case 'q':
                isContinue = false;
                responseMsg = $"{_promptsMap[nameof(PromptFactory.QuiteMatchMsg)]}\n\n{ShowMainMenu()}";
                break;
            default:
                isContinue = true;
                responseMsg = $"Invalid input.\n\n{_promptsMap[LatestGameState.ToString()]}";
                break;
        }

        return (isContinue, responseMsg);
    }

    public (bool continueGame, string responseMsg) HandleGameStateNoneInput(char keyChar)
    {
        bool isContinue = false;
        string responseMsg = string.Empty;

        switch(keyChar)
        {
            case 's':
                isContinue = true;
                responseMsg = Start();
                break;
            case 'c':
                isContinue = false;
                responseMsg = _promptsMap[nameof(PromptFactory.CloseGameMsg)];
                break;
            default:
                isContinue = true;
                responseMsg = $"Invalid input.\n\n{_promptsMap[LatestGameState.ToString()]}";
                break;
        }

        return (isContinue, responseMsg);
    }

    public async Task<string> ExecuteChampionTurnAsync()
    {
        string actionMsg = await _llmService.ExecuteTurnAsync(Champion, Challenger);

        LatestGameState = GameState.ChallengerTurn;

        return $"{actionMsg}\n\n{_promptsMap[LatestGameState.ToString()]}";
    }
}