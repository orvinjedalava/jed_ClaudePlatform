using GameScratch.Core.Common;
using GameScratch.Core.Common.Responses;

namespace GameScratch.Core.Services;

public interface IInputService
{
    PlayerOptionsResponse GetPlayerOptions(GameState gameState);
    PlayerOption GetPlayerOption(char keyChar, GameState gameState);
}