using GameScratch.Contracts.DTOs;
using GameScratch.Core.Common.Players;

namespace GameScratch.MinimalApi.Mappers;

public static class EquipmentMapper
{
    public static EquipmentDto ToDto(this Equipment equipment) => 
        new()
        {
            Weapon = WeaponMapper.ToDto(equipment.Weapon)
        };
}