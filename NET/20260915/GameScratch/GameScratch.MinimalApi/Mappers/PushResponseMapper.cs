using GameScratch.Contracts.DTOs;
using GameScratch.Contracts.Entities.Responses;

namespace GameScratch.MinimalApi.Mappers;

public static class PushResponseMapper
{
    public static PushResponseDto ToDto(this PushResponse response) => 
        new()
        {
            DefenderPoiseClass = response.DefenderPoiseClass,
            DefenderTotalPoiseClass = response.DefenderTotalPoiseClass,
            DefenderPoiseClassModifiers = response.DefenderPoiseClassModifiers,
            AttackerPushModifier = response.AttackerPushModifier,
            AttackerDiceRoll = response.AttackerDiceRoll,
            AttackerTotalDiceRoll = response.AttackerTotalDiceRoll,
            AttackerDiceRollModifiers = response.AttackerDiceRollModifiers
        };
}