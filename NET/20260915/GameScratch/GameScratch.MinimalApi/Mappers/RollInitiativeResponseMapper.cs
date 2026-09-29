using GameScratch.Contracts.DTOs;
using GameScratch.Contracts.Entities.Responses;

namespace GameScratch.MinimalApi.Mappers;

public static class RollInitiativeResponseMapper
{
    public static RollInitiativeResponseDto ToDto(this RollInitiativeResponse response) => 
        new()
        {
            DiceRoll = response.DiceRoll
        };
}