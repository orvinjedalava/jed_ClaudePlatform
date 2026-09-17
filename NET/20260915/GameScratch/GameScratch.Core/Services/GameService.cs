using GameScratch.Core.Common;
using GameScratch.Core.Common.Players;
using GameScratch.Core.Common.Weapons;
using GameScratch.Core.Common.Responses;
using GameScratch.Core.LLM;

namespace GameScratch.Core.Services;

public class GameService: IGameService
{
    private readonly ILLMService _llmService;
    private readonly IPlayerService _playerService;
    private readonly IInputService _inputService;

    private readonly Dictionary<string, string> _promptsMap;

    public GameService(
        ILLMService llmService,
        IPlayerService playerService,
        IInputService inputService)
    {
        _llmService = llmService ?? throw new ArgumentNullException("LLMService not injected.");
        _playerService = playerService ?? throw new ArgumentNullException("PlayerService not injected.");
        _inputService = inputService ?? throw new ArgumentNullException("InputService not injected.");

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

    public GameResponse ShowMainMenu()
    {
        LatestGameState = GameState.None;

        return new()
        {
            ContinueState = true,
            GameState = LatestGameState,
            PlayerOptions = _inputService.GetPlayerOptions(LatestGameState)
        };
    }

    public string Reset()
    {
        _llmService.ClearChatHistory();
        _playerService.ResetPlayer(Challenger);
        _playerService.ResetPlayer(Champion);

        LatestGameState = GameState.ChallengerTurn;

        return _promptsMap[LatestGameState.ToString()];
    }

    public GameResponse StartMatch(bool continueState)
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

        ( ActionResponse actionResponse, Player player) = _playerService.RollIniative(Challenger, Champion);

        LatestGameState = player.Profile.RoleType == RoleType.Challenger ? 
            GameState.ChallengerTurn 
            : GameState.ChampionTurn;

        return StartPlayerTurn(continueState, actionResponse.Message);
    }

    public GameResponse StartPlayerTurn(bool continueState, string message = "")
    {
        string preText = string.IsNullOrWhiteSpace(message) ? 
            ""
            : $"{message}\n\n";

        Player player = GetAttackingPlayer();
        ActionResponse response = _playerService.StartPlayerTurn(player);

        return new()
        {
            ContinueState = continueState,
            GameState = LatestGameState,
            Challenger = Challenger,
            Champion = Champion,
            PlayerOptions = _inputService.GetPlayerOptions(LatestGameState),
            Message = $"{preText}{response.Message}.",
            
        };
    }

    public GameResponse CloseGame(bool continueState)
    {
        LatestGameState = GameState.None;
        return new()
        {
            ContinueState = continueState,
            GameState = LatestGameState,
            Challenger = Challenger,
            Champion = Champion,
            Message = "Goodbye!"
        };
    }

    public GameResponse Surrender(bool continueState)
    {
        Player attacker = GetAttackingPlayer();
        Player defender = GetDefendingPlayer();
        LatestGameState = GameState.None;

        return new()
        {
            ContinueState = continueState,
            GameState = LatestGameState,
            Challenger = Challenger,
            Champion = Champion,
            Message = $"{attacker.Profile.Name} surrenders. {defender.Profile.Name} wins the match!"
        };
    }

    public Player GetAttackingPlayer()
    {
        switch(LatestGameState)
        {
            case GameState.ChallengerTurn:
                return Challenger;
            case GameState.ChampionTurn:
                return Champion;
            default:
                throw new NotImplementedException();
        }
    }

    public Player GetDefendingPlayer()
    {
        switch(LatestGameState)
        {
            case GameState.ChallengerTurn:
                return Champion;
            case GameState.ChampionTurn:
                return Challenger;
            default:
                throw new NotImplementedException();
        }
    }

    public GameResponse HandleInput(char keyChar)
    {
        PlayerOption option = _inputService.GetPlayerOption(keyChar, LatestGameState);

        switch(option.ServiceName)
        {
            case "Game":
                if (option.ActionName == "StartMatch")
                    return StartMatch(option.ContinueState);
                if (option.ActionName == "CloseGame")
                    return CloseGame(option.ContinueState);
                if (option.ActionName == "Surrender")
                    return Surrender(option.ContinueState);
                else
                    throw new NotImplementedException();
            case "Player":
                var attacker = GetAttackingPlayer();
                var defender = GetDefendingPlayer();
                var actionResponse = new ActionResponse();
                if (option.ActionName == "Attack")
                    actionResponse = _playerService.Attack(attacker, defender);
                if (option.ActionName == "Guard")
                    actionResponse = _playerService.Guard(attacker);
                if (option.ActionName == "EndTurn")
                    actionResponse = _playerService.EndTurn(attacker);
                
                if (actionResponse.SwitchPlayerTurn)
                {
                    SwitchPlayerTurn();
                }
                
                return new GameResponse()
                {
                    ContinueState = option.ContinueState,
                    GameState = LatestGameState,
                    Challenger = Challenger,
                    Champion = Champion,
                    PlayerOptions = _inputService.GetPlayerOptions(LatestGameState),
                    Message = "Please choose an action."
                };
            default:
                throw new NotImplementedException();
        }
    }

    public void SwitchPlayerTurn()
    {
        switch(LatestGameState)
        {
            case GameState.ChallengerTurn:
                LatestGameState = GameState.ChampionTurn;
                break;
            case GameState.ChampionTurn:
                LatestGameState = GameState.ChallengerTurn;
                break;
            default:
                throw new NotImplementedException();
        }
        
    }

    public async Task<GameResponse> ExecuteChampionTurnAsync()
    {
        GameResponse response = StartPlayerTurn(true);

        while(LatestGameState == GameState.ChampionTurn)
        {
            char keyChar = await _llmService.ChooseActionAsync(response);
            response = HandleInput(keyChar);
        }

        return LatestGameState == GameState.None ? 
            ShowMainMenu() 
            : StartPlayerTurn(true);
    }
}