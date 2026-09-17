using GameScratch.Core.Common.Weapons;

namespace GameScratch.Core.Common.Players;

public class Player
{
    public required Profile Profile { get; init; }
    public required Stats Stats { get; init; }
    public required Equipment Equipment { get; init; }
    public required Conditions Conditions { get; set; }

    public int GetTotalArmorClass()
    {
        return Stats.ArmorClass + GetArmorClassModifiers().Sum(x => x);
    }

    public List<int> GetArmorClassModifiers()
    {
        List<int> result = [];

        switch(Conditions.StanceType)
        {
            case StanceType.Guard:
                result.Add(5);
                break;
        }

        return result;
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

    public void StartTurn()
    {
        switch(Conditions.StanceType)
        {
            case StanceType.Default:
                Conditions.StaminaPointsDamage += 3;
                break;
            case StanceType.Guard:
                Conditions.StaminaPointsDamage += 1;
                break;
        }
        Conditions.StanceType = StanceType.Default;
    }

    public void UseWeapon()
    {
        Conditions.StaminaPointsDamage += Equipment.Weapon.StaminaCost;
    }

    public void ClearConditions()
    {
        Conditions = new Conditions();
    }

    public int GetInitiativeModifier()
    {
        return 0;
    }
    
}