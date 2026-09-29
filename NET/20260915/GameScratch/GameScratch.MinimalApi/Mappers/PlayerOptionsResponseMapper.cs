using GameScratch.Contracts.DTOs;
using GameScratch.Contracts.Entities.Responses;

namespace GameScratch.MinimalApi.Mappers;

public static class PlayerOptionsResponseMapper
{
    public static PlayerOptionsResponseDto ToDto(this PlayerOptionsResponse response) => 
        new()
        {
            Options = response.Options.ToDto()
        };
}