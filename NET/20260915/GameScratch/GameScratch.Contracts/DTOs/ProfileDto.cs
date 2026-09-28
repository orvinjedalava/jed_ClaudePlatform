namespace GameScratch.Contracts.DTOs;

public record ProfileDto
{
    public required string RoleType { get; set; }
    public required string Name { get; set; }
}