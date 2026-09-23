using GameScratch.Core.Common;
using GameScratch.Core.Common.Players;
using GameScratch.Core.Common.Weapons;
using GameScratch.Core.Common.Responses;
using GameScratch.Core.LLM;
using System.Text;
using GameScrach.Core.Common;

namespace GameScratch.Core.Services;

public class GameService: IGameService
{
    private readonly ILLMService _llmService;
    private readonly IPlayerService _playerService;
    private readonly IInputService _inputService;

    private readonly Dictionary<string, string> _promptsMap;

    public List<string> _history;

    public GameService(
        ILLMService llmService,
        IPlayerService playerService,
        IInputService inputService)
    {
        _llmService = llmService ?? throw new ArgumentNullException("LLMService not injected.");
        _playerService = playerService ?? throw new ArgumentNullException("PlayerService not injected.");
        _inputService = inputService ?? throw new ArgumentNullException("InputService not injected.");

        LatestGameState = GameState.None;
        GameMode = GameMode.None;
        LastGameResponse = new() { ContinueState = true, GameState = LatestGameState, GameMode = GameMode };
        
        _promptsMap = PromptFactory.Create().Build();
        _history = new List<string>();
    }

    public Player Challenger { get; set; } = null!;
    public Player Champion { get; set; } = null!;
    public GameState LatestGameState { get; set; }
    public GameResponse LastGameResponse { get; set; }
    public GameMode GameMode { get; set; }
    public char? LastChampionActionChoice { get; set; }
    public string LastLLMMessage { get; set; } = null!;

    public GameResponse ShowMainMenu()
    {
        LatestGameState = GameState.None;

        return LastGameResponse = new()
        {
            ContinueState = true,
            GameState = LatestGameState,
            History = GetLatestHistory(),
            GameMode = GameMode,
            Message = "Welcome to the Arena!",
            PlayerOptions = _inputService.GetPlayerOptions(LatestGameState)
        };
    }

    public string Reset()
    {
        _history.Clear();
        _playerService.ResetPlayer(Challenger);
        _playerService.ResetPlayer(Champion);

        LatestGameState = GameState.ChallengerTurn;

        return _promptsMap[LatestGameState.ToString()];
    }

    public GameResponse StartMatch(bool continueState)
    {
        _history.Clear(); 
        
        Challenger = _playerService.CreatePlayer(
            RoleType.Challenger,
            "Spartacus",
            WeaponType.ShortSword
        );

        Champion = _playerService.CreatePlayer(
            RoleType.Champion,
            "Dario",
            WeaponType.ShortSword
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
            : $"{message}";

        Player player = GetAttackingPlayer();
        ActionResponse response = _playerService.StartPlayerTurn(player);

        return new()
        {
            ContinueState = continueState,
            GameState = LatestGameState,
            History = GetLatestHistory(),
            GameMode = GameMode,
            Players = new() { Challenger = Challenger, Champion = Champion },
            PlayerOptions = _inputService.GetPlayerOptions(LatestGameState),
            Message = $"{preText}{response.Message}",
            
        };
    }

    public GameResponse CloseGame(bool continueState)
    {
        LatestGameState = GameState.None;
        return new()
        {
            ContinueState = continueState,
            GameState = LatestGameState,
            History = GetLatestHistory(),
            GameMode = GameMode,
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
            History = GetLatestHistory(),
            GameMode = GameMode, 
            Message = $"{attacker.Profile.Name} surrenders. {defender.Profile.Name} wins the match!",
            PlayerOptions = _inputService.GetPlayerOptions(LatestGameState)
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
        try
        {
            PlayerOption option = _inputService.GetPlayerOption(keyChar, LatestGameState);

            switch(option.ServiceName)
            {
                case "Game":
                    if (option.ActionName == ActionNames.StartSinglePlayerMatch)
                    {
                        GameMode = GameMode.SinglePlayer;
                        return LastGameResponse = StartMatch(option.ContinueState);
                    }
                    if (option.ActionName == ActionNames.StartTwoPlayerMatch)
                    {
                        GameMode = GameMode.TwoPlayers;
                        return LastGameResponse = StartMatch(option.ContinueState);
                    }
                    
                    if (option.ActionName == ActionNames.CloseGame)
                        return LastGameResponse = CloseGame(option.ContinueState);
                    if (option.ActionName == ActionNames.Surrender)
                        return LastGameResponse = Surrender(option.ContinueState);
                    else
                        throw new NotImplementedException();
                case "Player":
                    var attacker = GetAttackingPlayer();
                    var defender = GetDefendingPlayer();
                    var actionResponse = new ActionResponse();
                    if (option.ActionName == ActionNames.Attack)
                        actionResponse = _playerService.Attack(attacker, defender);
                    if (option.ActionName == ActionNames.Guard)
                        actionResponse = _playerService.Guard(attacker, defender);
                    if (option.ActionName == ActionNames.Wait)
                        actionResponse = _playerService.Wait(attacker, defender);

                    _history.Add(actionResponse.Message);
                    
                    if (actionResponse.SwitchPlayerTurn)
                    {
                        SwitchPlayerTurn();
                    }

                    bool isMatchOver = IsMatchOver(actionResponse);

                    return LastGameResponse = new GameResponse()
                    {
                        ContinueState = !isMatchOver && option.ContinueState,
                        GameState = LatestGameState,
                        GameMode = GameMode,
                        History = GetLatestHistory(),
                        Players = new() { Challenger = Challenger, Champion = Champion },
                        PlayerOptions = _inputService.GetPlayerOptions(LatestGameState),
                        Action = actionResponse,
                        Message = isMatchOver ? "Match is over!" : "The crowd roars!"
                    };

                default:
                    throw new NotImplementedException();
            }
        }
        catch
        {
            return LastGameResponse;
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

        _playerService.StartPlayerTurn(GetAttackingPlayer());
    }

    public bool IsMatchOver(ActionResponse actionResponse)
    {
        StringBuilder sb = new();

        sb.AppendLine(actionResponse.Message);

        bool isChallengerDefeated = Challenger.GetHitPointsRemaining() <= 0;
        bool isChamptionDefeated = Champion.GetHitPointsRemaining() <= 0;

        if (isChallengerDefeated || isChamptionDefeated)
        {
            LatestGameState = GameState.None;

            if (isChallengerDefeated && isChamptionDefeated)
            {
                sb.AppendLine("We have a draw!");
            }
            else if(isChamptionDefeated)
            {
                sb.AppendLine($"{Champion.Profile.Name} falls in defeat.");
                sb.AppendLine($"{Challenger.Profile.Name} wins!");
            }
            else if(isChallengerDefeated)
            {
                sb.AppendLine($"{Challenger.Profile.Name} is struck down.");
                sb.AppendLine($"{Champion.Profile.Name} wins!");
            }

            actionResponse.Message = sb.ToString();

            return true;
        }

        return false;
    }

    public async Task<GameResponse> ExecuteChampionTurnAsync()
    {
        if (LastChampionActionChoice == null)
        {
            return await GetChampionActionChoiceAsync();
        }
        else
        {
            return await GetChampionActionResultAsync();
        }
        
    }

    public List<string> GetLatestHistory()
    {
        return _history.TakeLast(6).ToList();
    }

    public async Task<GameResponse> GetChampionActionChoiceAsync()
    {
        // (char inputChar, string response) = await _llmService.SendMessageAsync(LastGameResponse);
        (char inputChar, string response) = await _llmService.GetToolChoiceAsync(LastGameResponse);

        LastChampionActionChoice = inputChar;
        LastLLMMessage = response;
        
        if (LastGameResponse?.Action != null)
        {
            LastGameResponse!.Action!.LLMMessage = response;
            LastGameResponse!.Action!.LLMActionChoice = inputChar;
            LastGameResponse!.Action!.Message = string.Empty;
        }
        else
        {
            LastGameResponse!.Message += $"\n{response}";
        }

        return LastGameResponse;
    }

    public async Task<GameResponse> GetChampionActionResultAsync()
    {
        await Task.Delay(3000);

        LastGameResponse = HandleInput(LastChampionActionChoice!.Value);

        // call LLM here to get a reaction

        if (LastGameResponse?.Action != null)
        {
            LastGameResponse!.Action!.LLMMessage = LastLLMMessage;
        }
            
        return LastGameResponse!;
    }

}