using GameScratch.Contracts.Entities.Responses;
using GameScratch.Contracts.Enums;

namespace GameScratch.Core.Services;

public interface IInputService
{
    PlayerOptionsResponse GetPlayerOptions(GameTurn gameTurn);
    PlayerOption GetPlayerOption(char keyChar, GameTurn gameTurn);
}