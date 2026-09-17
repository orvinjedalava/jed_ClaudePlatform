using GameScratch.Core.Common.Responses;
using GameScratch.Core.Common;

namespace GameScratch.Core.Services;

public class InputService : IInputService
{
    public static PlayerOption StartMatch => new() { Key = 's', Description = "Start Match", ActionName = "StartMatch", ServiceName = "Game", ContinueState = true };
    public static PlayerOption CloseGame => new() { Key = 'c', Description = "Close Game", ActionName = "CloseGame", ServiceName = "Game", ContinueState = false };
    public static PlayerOption Surrender => new() { Key = 'q', Description = "Surrender", ActionName = "Surrender", ServiceName = "Game", ContinueState = false };
    public static PlayerOption Attack => new() { Key = '1', Description = "Attack", ActionName = "Attack", ServiceName = "Player", ContinueState = true };
    public static PlayerOption Guard => new() { Key = '2', Description = "Guard", ActionName = "Guard", ServiceName = "Player", ContinueState = true };
    public static PlayerOption EndTurn => new() { Key = '3', Description = "End Turn", ActionName = "EndTurn", ServiceName = "Player", ContinueState = true };

    public static Dictionary<GameState, PlayerOptionsResponse> Map => new()
    {
        { GameState.None, new() { Options = [StartMatch, CloseGame] }},
        { GameState.ChallengerTurn, new() { Options = [Attack, Guard, EndTurn, Surrender ]}},
        { GameState.ChampionTurn, new() { Options = [Attack, Guard, EndTurn, Surrender ]}}
    };
    
    public PlayerOptionsResponse GetPlayerOptions(GameState gameState)
    {
        if (!Map.ContainsKey(gameState))
            throw new NotImplementedException($"GameState {gameState} not implemented.");

        return Map[gameState];
    }

    public PlayerOption GetPlayerOption(char keyChar, GameState gameState)
    {
        PlayerOption? option = GetPlayerOptions(gameState).Options.Find(x => x.Key == keyChar) ?? 
            throw new NotImplementedException($"Key '{keyChar}' not implemented for GameState {gameState}");

        return option;
    }
}