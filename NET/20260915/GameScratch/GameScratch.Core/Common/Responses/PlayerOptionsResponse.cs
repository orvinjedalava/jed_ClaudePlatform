namespace GameScratch.Core.Common.Responses;

public class PlayerOptionsResponse
{
    public required List<PlayerOption> Options { get; set; }
}

public class PlayerOption
{
    public required char Key { get; init; }
    public required string ActionName { get; init; }
    public required string ServiceName { get; init; }
    public required string Description { get; init; }
    public required bool ContinueState { get; init; }
}