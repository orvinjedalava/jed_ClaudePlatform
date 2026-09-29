using GameScratch.Contracts.DTOs;
using GameScratch.Core.Responses;

namespace GameScratch.MinimalApi.Mappers;

public static class StartTurnResponseMapper
{
    public static StartTurnResponseDto ToDto(this StartTurnResponse response) => 
        new()
        {
            Messages = response.Messages
        };
}