using GameScratch.Contracts.DTOs;
using GameScratch.Core.Responses;

namespace GameScratch.MinimalApi.Mappers;

public static class ActionResponseMapper
{
    public static ActionResponseDto ToDto(this ActionResponse response) => 
        new()
        {
            Message = response.Message,
            LLMMessage = response.LLMMessage,
            LLMActionChoice = response.LLMActionChoice,
            SwitchPlayerTurn = response.SwitchPlayerTurn,
            Attack = response.Attack?.ToDto(),
            RollInitiative = response.RollInitiative?.ToDto(),
        };
}