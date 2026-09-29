using GameScratch.Contracts.DTOs;
using GameScratch.Contracts.Entities.Players;

namespace GameScratch.MinimalApi.Mappers;

public static class EquipmentMapper
{
    public static EquipmentDto ToDto(this Equipment equipment) => 
        new()
        {
            Weapon = equipment.Weapon.ToDto()
        };
}