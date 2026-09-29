using GameScratch.Contracts.Entities.Players;
using GameScratch.Contracts.Enums;
using System.Text;

namespace GameScratch.Core.Extensions;

public static class PlayerExtensions
{
    public static string ToConsoleString(this Player player)
    {
        StringBuilder sb = new();

        sb.AppendLine($"****** The {player.Profile.RoleType} ******");
        sb.AppendLine();
        sb.AppendLine($"Name: {player.Profile.Name}");
        sb.AppendLine($"RoleType: {player.Profile.RoleType}");
        sb.AppendLine($"Status: {(string.IsNullOrWhiteSpace(player.GetStatus()) ? StanceType.Neutral.ToString() : player.GetStatus())}");
        sb.AppendLine($"StaminaPoints Remaining: {player.GetStaminaPointsRemaining()}");
        sb.AppendLine($"HitPoints Remaining: {player.GetHitPointsRemaining()}");
        sb.AppendLine($"ArmorClass: {player.Stats.ArmorClass}");
        sb.AppendLine($"BalanceClass: {player.Stats.BalanceClass}");
        sb.AppendLine($"PoiseClass: {player.Stats.PoiseClass}");
        sb.AppendLine($"Stance: {player.Conditions.StanceType}");
        sb.AppendLine($"Weapon: {player.Equipment.Weapon.Name}");
        sb.AppendLine($"Weapon StaminaPoints Cost: {player.Equipment.Weapon.StaminaCost}");
        sb.AppendLine($"Weapon HitPoints DamageDiceType: {player.Equipment.Weapon.HitPointsDamageDiceType}");
        sb.AppendLine($"Weapon StaminaPoints Damage: {player.Equipment.Weapon.StaminaPointsDamage}");
        sb.AppendLine($"Weapon Poise Damage Modifier: {player.Equipment.Weapon.PoiseDamageModifier}");
        sb.AppendLine();

        return sb.ToString();
    }

    public static int GetTotalArmorClass(this Player player)
    {
        return player.Stats.ArmorClass + player.GetArmorClassModifiers().Sum();
    }

    public static int GetTotalBalanceClass(this Player player, int attackerMissModifier = 0)
    {
        return player.Stats.BalanceClass + player.GetBalanceClassModifiers().Sum() - attackerMissModifier;
    }

    public static int GetTotalPoiseClass(this Player player, int attackerPushModifier = 0)
    {
        return player.Stats.PoiseClass + player.GetPoiseClassModifiers().Sum() - attackerPushModifier;
    }

    public static List<int> GetPoiseClassModifiers(this Player player)
    {
        List<int> result = [];

        switch(player.Conditions.StanceType)
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

        if (player.IsExhausted())
            result.Add(-3);

        return result;
    }

    public static List<int> GetArmorClassModifiers(this Player player)
    {
        List<int> result = [];

        switch(player.Conditions.StanceType)
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

        if (player.IsExhausted())
            result.Add(-3);

        return result;
    }

    public static List<int> GetBalanceClassModifiers(this Player player)
    {
        List<int> result = [];

        if (player.IsExhausted())
            result.Add(-3);

        return result;
    }

    public static List<int> GetAttackDiceRollModifiers(this Player player)
    {
        List<int> result = [];

        if (player.IsExhausted())
            result.Add(-3);

        return result;
    }

    public static List<int> GetCounterDiceRollModifiers(this Player player)
    {
        List<int> result = [];

        switch(player.Conditions.StanceType)
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

        if (player.IsExhausted())
            result.Add(-3);

        return result;
    }

    public static List<int> GetPushDiceRollModifiers(this Player player)
    {
        List<int> result = [];

        result.Add(player.Equipment.Weapon.PoiseDamageModifier);

        if (player.IsExhausted())
            result.Add(-3);

        return result;
    }

    public static int GetHitPointsRemaining(this Player player)
    {
        return player.Stats.HitPoints - player.Conditions.HitPointsDamage;
    }

    public static int GetStaminaPointsRemaining(this Player player)
    {
        return int.Min(player.Stats.StaminaPoints, player.Stats.StaminaPoints - player.Conditions.StaminaPointsDamage);
    }

    public static void AddHitPointsDamage(this Player player, int damage)
    {
        player.Conditions.HitPointsDamage += damage;
    }

    public static void AddStaminaPointsDamage(this Player player, int damage)
    {
        player.Conditions.StaminaPointsDamage += damage;
    }

    public static bool IsExhausted(this Player player)
    {
        return player.GetStaminaPointsRemaining() < 0;
    }

    public static void StartTurn(this Player player)
    {
        switch(player.Conditions.StanceType)
        {
            case StanceType.Neutral:
                player.Conditions.StaminaPointsDamage = int.Max(0, player.Conditions.StaminaPointsDamage - 2); 
                break;
            case StanceType.Guard:
                player.Conditions.StaminaPointsDamage = int.Max(0, player.Conditions.StaminaPointsDamage - 1); 
                break;
        }

        player.Conditions.StanceType = StanceType.Neutral;
    }

    public static void UseWeapon(this Player player)
    {
        player.AddStaminaPointsDamage(player.Equipment.Weapon.StaminaCost);
    }

    public static void ClearConditions(this Player player)
    {
        player.Conditions = new Conditions();
    }

    public static int GetInitiativeModifier(this Player player)
    {
        return 0;
    }

    public static string GetNameWithStatus(this Player player)
    {
        string status = player.GetStatus();

        if (string.IsNullOrEmpty(status))
            return $"{player.Profile.Name}";

        return $"{player.GetStatus()}-{player.Profile.Name}";
    }

    public static string GetStatus(this Player player)
    {
        List<string> statusList = [];

        if (player.GetHitPointsRemaining() > 0)
        {
            if (player.IsExhausted())
            statusList.Add("EXHAUSTED");
        
            switch(player.Conditions.StanceType)
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