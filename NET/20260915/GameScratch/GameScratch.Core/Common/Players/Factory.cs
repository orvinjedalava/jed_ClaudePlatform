using Anthropic.Models.Beta.Messages;
using GameScratch.Core.Common.Weapons;

namespace GameScratch.Core.Common.Players;

public class PlayerBuilder
{
    private Profile? _profile;
    private Equipment? _equipment;
    private Stats? _stats;

    public static PlayerBuilder Create() => new();

    public Player Build()
    {
        return new Player()
        {
            Profile = _profile ?? throw new ArgumentNullException("Profile is null"),
            Equipment = _equipment ?? throw new ArgumentNullException("Equipment is null"),
            Stats = _stats ?? throw new ArgumentNullException("Stats is null")
        };
    }

    public PlayerBuilder WithStats(int hitPoints = 20, int stamina = 10)
    {
        _stats = new Stats()
        {
            HitPoints = hitPoints,
            Stamina = stamina
        };

        return this;
    }

    public PlayerBuilder WithProfile(RoleType roleType, string name)
    {
        _profile = new()
        {
            RoleType = roleType,
            Name = name
        };

        return this;
    }

    public PlayerBuilder WithEquipment(Weapon weapon)
    {
        _equipment = new()
        {
            Weapon = weapon
        };

        return this;
    }
}