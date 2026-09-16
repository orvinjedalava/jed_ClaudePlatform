using GameScratch.Core.Common.Players;
using GameScratch.Core.Common.Weapons;
using GameScratch.Core.LLM;

namespace GameScratch.Core.Services;

public class GameService: IGameService
{
    private readonly ILLMService _llmService;
    private readonly IPlayerService _playerService;

    public GameService(
        ILLMService llmService,
        IPlayerService playerService)
    {
        _llmService = llmService ?? throw new ArgumentNullException("LLMService not injected.");
        _playerService = playerService ?? throw new ArgumentNullException("PlayerService not injected.");

        Challenger = new Player
        {
            Profile = new(),
            Stats = new(),
            Equipment = new()
            {
                Weapon = WeaponBuilder.Create().FromWeaponType(WeaponType.BareHands).Build()
            }
        };
    }

    public Player Challenger { get; set; } = null!;
    public Player Champion { get; set; } = null!;

    async Task<string> IGameService.SendMessageToLLMAsync(string message)
    {
        return await _llmService.SendMessageAsync(message);
    }

    void IGameService.Reset()
    {
        _llmService.ClearChatHistory();
        _playerService.ResetPlayer(Challenger);
        _playerService.ResetPlayer(Champion);
    }
}