using System.Text;
using Anthropic.Models.Beta.Messages;
using GameScratch.Core.Common.Weapons;

namespace GameScratch.Core.Common.Players;

public class Player
{
    public required Profile Profile { get; init; }
    public required Stats Stats { get; init; }
    public required Equipment Equipment { get; init; }
    public required Conditions Conditions { get; set; }

    public string ToConsoleString()
    {
        StringBuilder sb = new();

        sb.AppendLine($"*** The {Profile.RoleType} ***");
        sb.AppendLine($"Name: {Profile.Name}");
        sb.AppendLine($"ArmorClass: {Stats.ArmorClass}");
        sb.AppendLine($"HitPoints Remaining: {GetHitPointsRemaining()}");
        sb.AppendLine($"StaminaPoints Remaining: {GetStaminaPointsRemaining()}");
        sb.AppendLine($"Stance: {Conditions.StanceType}");
        sb.AppendLine($"Weapon: {Equipment.Weapon.Name}");
        sb.AppendLine($"Weapon StaminaPoints Cost: {Equipment.Weapon.StaminaCost}");
        sb.AppendLine($"Weapon BaseDamage Dice: {Equipment.Weapon.BaseDamage}");

        return sb.ToString();
    }

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
                result.Add(2);
                break;
            case StanceType.Exhausted:
                result.Add(-2);
                break;
        }

        return result;
    }

    public List<int> GetAttackDiceRollModifiers()
    {
        List<int> result = [];
        return result;
    }

    public int GetHitPointsRemaining()
    {
        return Stats.HitPoints - Conditions.HitPointsDamage;
    }

    public int GetStaminaPointsRemaining()
    {
        return int.Min(Stats.StaminaPoints, Stats.StaminaPoints - Conditions.StaminaPointsDamage);
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
                Conditions.StaminaPointsDamage = int.Max(0, Conditions.StaminaPointsDamage - 2); 
                break;
            case StanceType.Guard:
                Conditions.StaminaPointsDamage = int.Max(0, Conditions.StaminaPointsDamage - 1); 
                break;
            case StanceType.Exhausted:
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