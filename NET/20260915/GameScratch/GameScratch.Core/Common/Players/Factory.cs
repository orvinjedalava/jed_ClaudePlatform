using Anthropic.Models.Beta.Messages;
using GameScratch.Core.Common.Weapons;

namespace GameScratch.Core.Common.Players;

public class PlayersFactory
{
    public static Player DefaultPlayer =>
        PlayerBuilder
            .Create()
            .WithProfile(RoleType.None, string.Empty)
            .WithStats()
            .WithEquipment(WeaponsFactory.BareHands)
            .WithConditions()
            .Build();
            
    public static Player DefaultChallengerPlayer =>
        PlayerBuilder
            .Create()
            .WithProfile(RoleType.Challenger, RoleType.Challenger.ToString())
            .WithStats()
            .WithEquipment(WeaponsFactory.BareHands)
            .WithConditions()
            .Build();

    public static Player DefaultChampionPlayer =>
        PlayerBuilder
            .Create()
            .WithProfile(RoleType.Champion, RoleType.Champion.ToString())
            .WithStats()
            .WithEquipment(WeaponsFactory.BareHands)
            .WithConditions()
            .Build();
}

public class PlayerBuilder
{
    private Profile? _profile;
    private Equipment? _equipment;
    private Stats? _stats;
    private Conditions? _conditions;

    public static PlayerBuilder Create() => new();

    public Player Build()
    {
        return new Player()
        {
            Profile = _profile ?? throw new ArgumentNullException("Profile is null"),
            Equipment = _equipment ?? throw new ArgumentNullException("Equipment is null"),
            Stats = _stats ?? throw new ArgumentNullException("Stats is null"),
            Conditions = _conditions ?? throw new ArgumentNullException("Conditions is null"),
        };
    }

    public PlayerBuilder WithStats(int hitPoints = 20, int staminaPoints = 2, int armorClass = 10)
    {
        _stats = new Stats()
        {
            HitPoints = hitPoints,
            StaminaPoints = staminaPoints,
            ArmorClass = armorClass
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

    public PlayerBuilder WithConditions()
    {
        _conditions = new()
        {
            StanceType = StanceType.Default
        };

        return this;
    }
}