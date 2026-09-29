using GameScratch.Contracts.DTOs;
using GameScratch.Contracts.Entities.Weapons;

namespace GameScratch.MinimalApi.Mappers;

public static class WeaponMapper
{
    public static WeaponDto ToDto(this Weapon weapon) => 
        new()
        {
            HitPointsDamageDiceType = weapon.HitPointsDamageDiceType.ToString(),
            StaminaPointsDamage = weapon.StaminaPointsDamage,
            Name = weapon.Name,
            StaminaCost = weapon.StaminaCost,
            PoiseDamageModifier = weapon.PoiseDamageModifier,
            WeaponType = weapon.WeaponType.ToString(),
        };
}