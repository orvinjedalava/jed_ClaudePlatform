namespace GameScratch.Contracts.DTOs;

public record PlayerOptionsResponseDto
{
    public required List<PlayerOptionDto> Options { get; set; }
}