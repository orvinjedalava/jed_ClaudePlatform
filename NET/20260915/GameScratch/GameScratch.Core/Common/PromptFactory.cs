namespace GameScratch.Core.Common;

public class PromptFactory
{
    public const string ChallengerTurnMsg = 
    """
        [1] Attack with weapon
        [2] Go to guard stance
    """;

    public const string ChampionTurnMsg = 
    """
        Model is thinking...
    """;

    public static PromptFactory Create() => new();

    public Dictionary<GameState, string> Build()
    {
        var _promptsMap = new Dictionary<GameState, string>()
        {
            { GameState.ChallengerTurn, ChallengerTurnMsg },
            { GameState.ChampionTurn, ChampionTurnMsg }
        };

        return _promptsMap;
    }
}