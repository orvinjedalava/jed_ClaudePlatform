using GameScratch.Core.LLM;
using GameScratch.Core.Services;
using GameScratch.Core.Common.Players;
using GameScratch.Core.Common.Weapons;
using Moq;

namespace GameScratch.Tests.LLM;

public class LLMServiceBaseTests
{
    private ILLMService _llmServiceBase;

    private Mock<IPlayerService> _playerServiceMock;

    public LLMServiceBaseTests()
    {
        _playerServiceMock = new Mock<IPlayerService>();

        _llmServiceBase = new LLMServiceBase(_playerServiceMock.Object);
    }

    [Fact]
    public void ClearChatHistory_Success()
    {
        _llmServiceBase.ClearChatHistory();

        Assert.Empty(_llmServiceBase.ChatHistory);
    }

    // [Fact]
    // public async Task ExecuterTurnAsync_Success()
    // {
    //     var actions = new Dictionary<string, Func<IActionContext, string>>
    //     {
    //         { "Attack", ctx => $"{ctx.Attacker.Profile.Name} attacks {ctx.Defender.Profile.Name}" },
    //         { "GuardStance", ctx => $"{ctx.Attacker.Profile.Name} guards" }
    //     };

    //     _playerServiceMock.SetupGet(s => s.Actions).Returns(actions);

    //     Weapon weapon = WeaponBuilder.Create().FromWeaponType(WeaponType.BareHands).Build();

    //     Player challenger = PlayersFactory.DefaultChallengerPlayer;
    //     Player champion = PlayersFactory.DefaultChampionPlayer;

    //     Assert.NotNull(await _llmServiceBase.ExecuteTurnAsync(champion, challenger));
    // }
}