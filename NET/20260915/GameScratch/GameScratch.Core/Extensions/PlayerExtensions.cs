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
}