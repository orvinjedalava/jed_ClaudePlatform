using GameScratch.Contracts.DTOs;
using GameScratch.Core.Common.Responses;

namespace GameScratch.MinimalApi.Mappers;

public static class AttackResponseMapper
{
    public static AttackResponseDto ToDto(this AttackResponse response) => 
        new()
        {
            DefenderArmorClass = response.DefenderArmorClass,
            DefenderTotalArmorClass = response.DefenderTotalArmorClass,
            DefenderArmorClassModifiers = response.DefenderArmorClassModifiers,
            AttackerDiceRoll = response.AttackerDiceRoll,
            AttackerTotalDiceRoll = response.AttackerTotalDiceRoll,
            Counter = response.Counter?.ToDto()
        };
}