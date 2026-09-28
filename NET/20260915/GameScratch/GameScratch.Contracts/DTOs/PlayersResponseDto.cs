namespace GameScratch.Contracts.DTOs;

public record PlayersResponseDto
{
    public PlayerDto? Challenger { get; init; }
    public PlayerDto? Champion { get; init; }
}