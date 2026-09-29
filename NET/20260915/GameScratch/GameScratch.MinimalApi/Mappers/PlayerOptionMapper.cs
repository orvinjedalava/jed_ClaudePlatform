using GameScratch.Contracts.DTOs;
using GameScratch.Contracts.Entities.Responses;

namespace GameScratch.MinimalApi.Mappers;

public static class PlayerOptionMapper
{
    public static PlayerOptionDto ToDto(this PlayerOption option) => 
        new()
        {
            Key = option.Key,
            ActionName = option.ActionName,
            ServiceName = option.ServiceName,
            Description = option.Description,
            LLMDescription = option.LLMDescription,
            ContinueState = option.ContinueState
        };

    public static List<PlayerOptionDto> ToDto(this List<PlayerOption> options) =>
        [.. options.Select(option => option.ToDto())];
}