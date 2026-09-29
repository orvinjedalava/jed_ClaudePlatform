using GameScratch.Contracts.DTOs;
using GameScratch.Contracts.Entities.Players;

namespace GameScratch.MinimalApi.Mappers;

public static class PlayerMapper
{
    public static PlayerDto ToDto(this Player player) => 
        new()
        {
            Profile = player.Profile.ToDto(),
            Stats = player.Stats.ToDto(),
            Equipment = player.Equipment.ToDto(),
            Conditions = player.Conditions.ToDto()
        };
}