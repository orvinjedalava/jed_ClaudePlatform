using GameScratch.Contracts.DTOs;
using GameScratch.Core.Common.Players;

namespace GameScratch.MinimalApi.Mappers;

public static class StatsMapper
{
    public static StatsDto ToDto(this Stats stats) => 
        new()
        {
            HitPoints = stats.HitPoints,
            StaminaPoints = stats.StaminaPoints,
            ArmorClass = stats.ArmorClass,
            BalanceClass = stats.BalanceClass,
            PoiseClass = stats.PoiseClass
        };
}