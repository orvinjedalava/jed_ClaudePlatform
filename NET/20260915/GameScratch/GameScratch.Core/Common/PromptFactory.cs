namespace GameScratch.Core.Common;

public class PromptFactory
{
    public const string ChallengerTurnMsg = 
    """
    =========================================================================
        [1] Attack with weapon
        [2] Go to guard stance

        [r] Reset Match
        [q] Quite Match
        
    """;

    public const string ChampionTurnMsg = 
    """
    =========================================================================
        Model is thinking...
    """;

    public const string GameStateNoneMsg = 
    """
    =========================================================================
        [s] Start game
        [c] Close game
    """;

    public const string ChampionActionMsg = 
    """
    =========================================================================
        Champion decided to {0}
    """;


    public const string QuiteMatchMsg = "Quiting current match...";
    public const string CloseGameMsg = "Closing game. Goodbye!";

    public static PromptFactory Create() => new();

    public Dictionary<string, string> Build()
    {
        var _promptsMap = new Dictionary<string, string>()
        {
            { GameState.ChallengerTurn.ToString(), ChallengerTurnMsg },
            { GameState.ChampionTurn.ToString(), ChampionTurnMsg },
            { GameState.None.ToString(), GameStateNoneMsg},
            { nameof(QuiteMatchMsg), QuiteMatchMsg },
            { nameof(ChampionActionMsg), ChampionActionMsg},
            { nameof(CloseGameMsg), CloseGameMsg },
        };

        return _promptsMap;
    }
}