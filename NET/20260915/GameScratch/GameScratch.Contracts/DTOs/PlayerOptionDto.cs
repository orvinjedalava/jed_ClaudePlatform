namespace GameScratch.Contracts.DTOs;

public record PlayerOptionDto
{
    public required char Key { get; init; }
    public required string ActionName { get; init; }
    public required string ServiceName { get; init; }
    public required string Description { get; init; }
    public required string LLMDescription { get; init; }
    public required bool ContinueState { get; init; }
}