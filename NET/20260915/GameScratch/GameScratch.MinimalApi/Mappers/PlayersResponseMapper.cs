using GameScratch.Contracts.DTOs;
using GameScratch.Core.Common.Players;
using GameScratch.Core.Common.Responses;
using Microsoft.AspNetCore.Mvc;

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