using GameScratch.Core.Common.Weapons;

namespace GameScratch.Core.Common.Players;

public class Player
{
    public required Profile Profile { get; init; }
    public required Stats Stats { get; init; }
    public required Equipment Equipment { get; init; }
    public required Conditions Conditions { get; set; }

    public int GetArmorClass()
    {
        return Stats.ArmorClass;
    }

    public int GetHitPointsRemaining()
    {
        return Stats.HitPoints - Conditions.HitPointsDamage;
    }

    public int GetStaminaPointsRemaining()
    {
        return Stats.StaminaPoints - Conditions.StaminaPointsDamage;
    }

    public void AddHitPointsDamage(int damage)
    {
        Conditions.HitPointsDamage += damage;
    }

    public void UseWeapon()
    {
        Conditions.StaminaPointsDamage += Equipment.Weapon.StaminaCost;
    }

    public void ClearConditions()
    {
        Conditions = new Conditions();
    }
    
}