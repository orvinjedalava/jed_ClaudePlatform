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

        sb.AppendLine($"****** The {Profile.RoleType} ******");
        sb.AppendLine();
        sb.AppendLine($"Name: {Profile.Name}");
        sb.AppendLine($"RoleType: {Profile.RoleType}");
        sb.AppendLine($"Status: {(string.IsNullOrWhiteSpace(GetStatus()) ? StanceType.Neutral.ToString() : GetStatus())}");
        sb.AppendLine($"StaminaPoints Remaining: {GetStaminaPointsRemaining()}");
        sb.AppendLine($"HitPoints Remaining: {GetHitPointsRemaining()}");
        sb.AppendLine($"ArmorClass: {Stats.ArmorClass}");
        sb.AppendLine($"BalanceClass: {Stats.BalanceClass}");
        sb.AppendLine($"PoiseClass: {Stats.PoiseClass}");
        sb.AppendLine($"Stance: {Conditions.StanceType}");
        sb.AppendLine($"Weapon: {Equipment.Weapon.Name}");
        sb.AppendLine($"Weapon StaminaPoints Cost: {Equipment.Weapon.StaminaCost}");
        sb.AppendLine($"Weapon HitPoints DamageDiceType: {Equipment.Weapon.HitPointsDamageDiceType}");
        sb.AppendLine($"Weapon StaminaPoints Damage: {Equipment.Weapon.StaminaPointsDamage}");
        sb.AppendLine($"Weapon Poise Damage Modifier: {Equipment.Weapon.PoiseDamageModifier}");
        sb.AppendLine();

        return sb.ToString();
    }

    public int GetTotalArmorClass()
    {
        return Stats.ArmorClass + GetArmorClassModifiers().Sum();
    }

    public int GetTotalBalanceClass(int attackerMissModifier = 0)
    {
        return Stats.BalanceClass + GetBalanceClassModifiers().Sum() - attackerMissModifier;
    }

    public int GetTotalPoiseClass(int attackerPushModifier = 0)
    {
        return Stats.PoiseClass + GetPoiseClassModifiers().Sum() - attackerPushModifier;
    }

    public List<int> GetPoiseClassModifiers()
    {
        List<int> result = [];

        switch(Conditions.StanceType)
        {
            case StanceType.Guard:
                result.Add(2);
                break;
            case StanceType.Unbalanced:
                result.Add(-2);
                break;
            case StanceType.Staggered:
                result.Add(-2);
                break;
        }

        if (IsExhausted())
            result.Add(-3);

        return result;
    }

    public List<int> GetArmorClassModifiers()
    {
        List<int> result = [];

        switch(Conditions.StanceType)
        {
            case StanceType.Guard:
                result.Add(2);
                break;
            case StanceType.Unbalanced:
                result.Add(-2);
                break;
            case StanceType.Staggered:
                result.Add(-2);
                break;
        }

        if (IsExhausted())
            result.Add(-3);

        return result;
    }

    public List<int> GetBalanceClassModifiers()
    {
        List<int> result = [];

        if (IsExhausted())
            result.Add(-3);

        return result;
    }

    public List<int> GetAttackDiceRollModifiers()
    {
        List<int> result = [];

        if (IsExhausted())
            result.Add(-3);

        return result;
    }

    public List<int> GetCounterDiceRollModifiers()
    {
        List<int> result = [];

        switch(Conditions.StanceType)
        {
            case StanceType.Guard:
                result.Add(-1);
                break;
            case StanceType.Unbalanced:
                result.Add(-3);
                break;
            case StanceType.Staggered:
                result.Add(-3);
                break;
        }

        if (IsExhausted())
            result.Add(-3);

        return result;
    }

    public List<int> GetPushDiceRollModifiers()
    {
        List<int> result = [];

        result.Add(Equipment.Weapon.PoiseDamageModifier);

        if (IsExhausted())
            result.Add(-3);

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

    public void AddStaminaPointsDamage(int damage)
    {
        Conditions.StaminaPointsDamage += damage;
    }

    public bool IsExhausted()
    {
        return GetStaminaPointsRemaining() < 0;
    }

    public void StartTurn()
    {
        switch(Conditions.StanceType)
        {
            case StanceType.Neutral:
                Conditions.StaminaPointsDamage = int.Max(0, Conditions.StaminaPointsDamage - 2); 
                break;
            case StanceType.Guard:
                Conditions.StaminaPointsDamage = int.Max(0, Conditions.StaminaPointsDamage - 1); 
                break;
        }

        Conditions.StanceType = StanceType.Neutral;
    }

    public void UseWeapon()
    {
        AddStaminaPointsDamage(Equipment.Weapon.StaminaCost);
    }

    public void ClearConditions()
    {
        Conditions = new Conditions();
    }

    public int GetInitiativeModifier()
    {
        return 0;
    }

    public string GetNameWithStatus()
    {
        string status = GetStatus();

        if (string.IsNullOrEmpty(status))
            return $"{Profile.Name}";

        return $"{GetStatus()}-{Profile.Name}";
    }

    public string GetStatus()
    {
        List<string> statusList = [];

        if (GetHitPointsRemaining() > 0)
        {
            if (IsExhausted())
            statusList.Add("EXHAUSTED");
        
            switch(Conditions.StanceType)
            {
                case StanceType.Guard:
                    statusList.Add("GUARDED");
                    break;
                case StanceType.Staggered:
                    statusList.Add("STAGGERED");
                    break;
                case StanceType.Unbalanced:
                    statusList.Add("UNBALANCED");
                    break;
            }
        }
        else
        {
            statusList.Add("DEAD");
        }

        return statusList.Count > 0 ? $"[{string.Join(",", statusList)}]" : string.Empty;
    }
    
}