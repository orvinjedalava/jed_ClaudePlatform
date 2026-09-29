using GameScratch.Contracts.DTOs;
using GameScratch.Contracts.Entities.Players;

namespace GameScratch.MinimalApi.Mappers;

public static class ConditionsMapper
{
    public static ConditionsDto ToDto(this Conditions conditions) => 
        new()
        {
            StanceType = conditions.StanceType.ToString(),
            HitPointsDamage = conditions.HitPointsDamage,
            StaminaPointsDamage = conditions.StaminaPointsDamage
        };
}