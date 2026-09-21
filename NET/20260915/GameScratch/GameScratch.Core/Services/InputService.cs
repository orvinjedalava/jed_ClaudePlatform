using GameScratch.Core.Common.Responses;
using GameScratch.Core.Common;

namespace GameScratch.Core.Services;

public class InputService : IInputService
{
    public static PlayerOption StartSinglePlayerMatch => new() { Key = '1', Description = "Start Single Player Match", ActionName = ActionNames.StartSinglePlayerMatch, ServiceName = "Game", ContinueState = true };
    public static PlayerOption StartTwoPlayerMatch => new() { Key = '2', Description = "Start Two Player Match", ActionName = ActionNames.StartTwoPlayerMatch, ServiceName = "Game", ContinueState = true };
    public static PlayerOption CloseGame => new() { Key = 'x', Description = "Close Game", ActionName = ActionNames.CloseGame, ServiceName = "Game", ContinueState = false };
    public static PlayerOption Surrender => new() { Key = 'q', Description = "Surrender and admit defeat. ( Ends the match )", ActionName = ActionNames.Surrender, ServiceName = "Game", ContinueState = false };
    public static PlayerOption Attack => new() { Key = '1', Description = "Attack with your weapon." , ActionName = ActionNames.Attack, ServiceName = "Player", ContinueState = true };
    public static PlayerOption Guard => new() { Key = '2', Description = "Brace yourself and go to Guard Stance. ( Ends your turn )", ActionName = ActionNames.Guard, ServiceName = "Player", ContinueState = true };
    public static PlayerOption Wait => new() { Key = '3', Description = "Wait and go to Neutral Stance. ( Ends your turn )", ActionName = ActionNames.Wait, ServiceName = "Player", ContinueState = true };

    public static Dictionary<GameState, PlayerOptionsResponse> Map => new()
    {
        { GameState.None, new() { Options = [StartSinglePlayerMatch, StartTwoPlayerMatch, CloseGame] }},
        { GameState.ChallengerTurn, new() { Options = [Attack, Guard, Wait, Surrender ]}},
        { GameState.ChampionTurn, new() { Options = [Attack, Guard, Wait, Surrender ]}}
    };
    
    public PlayerOptionsResponse GetPlayerOptions(GameState gameState)
    {
        if (!Map.ContainsKey(gameState))
            throw new NotImplementedException($"GameState {gameState} not implemented.");

        return Map[gameState];
    }

    public PlayerOption GetPlayerOption(char keyChar, GameState gameState)
    {
        char normalizedKeyChar = char.ToLowerInvariant(keyChar);

        PlayerOption? option = GetPlayerOptions(gameState).Options.Find(x => char.ToLowerInvariant(x.Key) == normalizedKeyChar) ?? 
            throw new NotImplementedException($"Key '{keyChar}' not implemented for GameState {gameState}");

        return option;
    }
}