using GameScratch.Core.Common.Game;
using GameScratch.Core.Common.Responses;

namespace GameScratch.Core.Services;

public interface IInputService
{
    PlayerOptionsResponse GetPlayerOptions(GameTurn gameTurn);
    PlayerOption GetPlayerOption(char keyChar, GameTurn gameTurn);
}