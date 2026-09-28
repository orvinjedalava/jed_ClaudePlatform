using GameScratch.Contracts.DTOs;
using GameScratch.Core.Common.Players;

namespace GameScratch.MinimalApi.Mappers;

public static class PlayerMapper
{
    public static PlayerDto ToDto(this Player player) => 
        new()
        {
            Profile = ProfileMapper.ToDto(player.Profile),
            Stats = StatsMapper.ToDto(player.Stats),
            Equipment = EquipmentMapper.ToDto(player.Equipment),
            Conditions = ConditionsMapper.ToDto(player.Conditions)
        };
}