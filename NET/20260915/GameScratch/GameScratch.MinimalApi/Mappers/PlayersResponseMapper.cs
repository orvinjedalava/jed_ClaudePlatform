using GameScratch.Contracts.DTOs;
using GameScratch.Contracts.Entities.Responses;

namespace GameScratch.MinimalApi.Mappers;

public static class PlayersResponseMapper
{
    public static PlayersResponseDto ToDto(this PlayersResponse response) => 
        new()
        {
            Challenger = response.Challenger?.ToDto(),
            Champion = response.Champion?.ToDto()
        };
}