using GameScratch.Core.Common.Weapons;

namespace GameScratch.Core.Common.Players;

public class Player
{
    public required Profile Profile { get; init; }
    public required Stats Stats { get; init; }
    public required Equipment Equipment { get; init; }
}