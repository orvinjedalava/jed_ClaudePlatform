using GameScratch.Contracts.DTOs;
using GameScratch.Contracts.Entities.Responses;

namespace GameScratch.MinimalApi.Mappers;

public static class GameResponseMapper
{
    public static GameResponseDto ToDto(this GameResponse response) =>
        new()
        {
            ContinueState = response.ContinueState,
            GameTurn = response.GameTurn.ToString(),
            GameMode = response.GameMode.ToString(),
            Message = response.Message,
            Players = response.Players?.ToDto(),
            Action = response.Action?.ToDto(),
            StartTurn = response.StartTurn?.ToDto(),
            PlayerOptions = response.PlayerOptions?.ToDto()
        };
}