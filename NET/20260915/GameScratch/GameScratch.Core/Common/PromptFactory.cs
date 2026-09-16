namespace GameScratch.Core.Common;

public class PromptFactory
{
    public const string ChallengerTurnMsg = 
    """
        [1] Attack with weapon
        [2] Go to guard stance

        [r] Reset Game
        [q] Quite Game
        
    """;

    public const string ChampionTurnMsg = 
    """
        Model is thinking...
    """;

    public const string GameStateNoneMsg = 
    """
        [s] Start game
        [q] Quite game
    """;

    public const string ChampionActionMsg = 
    """
        Champion decided to {0}
    """;


    public const string QuiteMsg = "Closing game...";

    public static PromptFactory Create() => new();

    public Dictionary<string, string> Build()
    {
        var _promptsMap = new Dictionary<string, string>()
        {
            { GameState.ChallengerTurn.ToString(), ChallengerTurnMsg },
            { GameState.ChampionTurn.ToString(), ChampionTurnMsg },
            { GameState.None.ToString(), GameStateNoneMsg},
            { nameof(QuiteMsg), QuiteMsg },
            { nameof(ChampionActionMsg), ChampionActionMsg}
        };

        return _promptsMap;
    }
}