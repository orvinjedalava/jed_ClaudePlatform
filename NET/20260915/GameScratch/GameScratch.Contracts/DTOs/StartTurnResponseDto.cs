namespace GameScratch.Contracts.DTOs;

public record StartTurnResponseDto
{
    public Dictionary<string, string>? Messages { get; set; }
}
