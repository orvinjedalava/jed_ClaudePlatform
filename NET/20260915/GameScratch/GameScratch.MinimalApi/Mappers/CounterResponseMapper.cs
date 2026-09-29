using GameScratch.Contracts.DTOs;
using GameScratch.Core.Responses;

namespace GameScratch.MinimalApi.Mappers;

public static class CounterResponseMapper
{
    public static CounterResponseDto ToDto(this CounterResponse response) => 
        new()
        {
            AttackerBalanceClass = response.AttackerBalanceClass,
            AttackerTotalBalanceClass = response.AttackerTotalBalanceClass,
            AttackerBalanceClassModifiers = response.AttackerBalanceClassModifiers,
            AttackerMissModifier = response.AttackerMissModifier,
            DefenderDiceRoll = response.DefenderDiceRoll,
            DefenderTotalDiceRoll = response.DefenderTotalDiceRoll,
            DefenderDiceRollModifiers = response.DefenderDiceRollModifiers
        };
}