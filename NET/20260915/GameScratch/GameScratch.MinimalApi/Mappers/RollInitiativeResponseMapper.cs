using GameScratch.Contracts.DTOs;
using GameScratch.Core.Responses;

namespace GameScratch.MinimalApi.Mappers;

public static class RollInitiativeResponseMapper
{
    public static RollInitiativeResponseDto ToDto(this RollInitiativeResponse response) => 
        new()
        {
            DiceRoll = response.DiceRoll
        };
}