using GameScratch.Contracts.Enums;

namespace GameScratch.Contracts.Entities.Players;

public class Profile
{
    public RoleType RoleType { get; set; } = RoleType.None;
    public string Name { get; set; } = string.Empty;
}