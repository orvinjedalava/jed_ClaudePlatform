using System.Text;
using GameScratch.Contracts.Enums;

namespace GameScratch.Contracts.Entities.Players;

public class Player
{
    public required Profile Profile { get; init; }
    public required Stats Stats { get; init; }
    public required Equipment Equipment { get; init; }
    public required Conditions Conditions { get; set; }    
}