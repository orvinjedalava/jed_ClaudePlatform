using Moq;

using GameScratch.Core.Services;
using GameScratch.Core.LLM;
using GameScratch.Core.Common;
using GameScratch.Core.Common.Players;
using GameScratch.Core.Common.Responses;

namespace GameScratch.Tests.Services;

public class GameServiceTests
{
    private readonly IGameService _gameService;
    private readonly Mock<ILLMService> _llmServiceMock;
    private readonly Mock<IPlayerService> _playerServiceMock;
    private readonly Mock<IInputService> _inputServiceMock;

    public GameServiceTests()
    {
        // initialize mocks
        _llmServiceMock = new Mock<ILLMService>();
        _playerServiceMock = new Mock<IPlayerService>();
        _inputServiceMock = new Mock<IInputService>();

        _gameService = new GameService(
            llmService: _llmServiceMock.Object,
            playerService: _playerServiceMock.Object,
            inputService: _inputServiceMock.Object
        );

        _gameService.Challenger = PlayersFactory.DefaultChallengerPlayer;
        _gameService.Champion = PlayersFactory.DefaultChampionPlayer;
        
    }

    // [Fact]
    // public async Task SendMessageToLLMAsync_Success()
    // {
    //     string expectedResponse = "Mocked Response";
    //     string message = "My message";

    //     _llmServiceMock
    //         .Setup(m => m.SendMessageAsync(message))
    //         .ReturnsAsync(expectedResponse);

    //     var result = await _gameService.SendMessageToLLMAsync(message);

    //     Assert.Equal(expectedResponse, result);
    // }

    [Fact]
    public void Reset_Success()
    {
        var exception = Record.Exception(() => _gameService.Reset());

        Assert.Null(exception);
    }

    // [Fact]
    // public void Start_Success()
    // {
    //     var exception = Record.Exception(() => _gameService.StartMatch(true));

    //     Assert.Null(exception);
    // }

    // [Fact]
    // public async Task ExecuteChampionTurn_Success()
    // {
    //     _gameService.LatestGameState = GameState.ChampionTurn;
    //     // _playerServiceMock
    //     //     .Setup(m => m.StartPlayerTurn(It.IsAny<Player>()))
    //     //     .Returns(new Core.Common.Responses.ActionResponse());
    //     // _inputServiceMock
    //     //     .Setup(m => m.GetPlayerOptions(It.IsAny<GameState>()))
    //     //     .Returns(new Core.Common.Responses.PlayerOptionsResponse() {Options = []});
    //     _llmServiceMock
    //         .Setup(m => m.ChooseActionAsync(It.IsAny<GameResponse>()))
    //         .ReturnsAsync('q');
    //     // _inputServiceMock
    //     //     .Setup(m=> m.GetPlayerOption(It.IsAny<char>(), It.IsAny<GameState>()))
    //     //     .Returns(GameScratch.Core.Services.InputService.Surrender);

    //    await _gameService.ExecuteChampionTurnAsync();

    //    Assert.Equal(GameState.ChampionTurn, _gameService.LatestGameState); 
    // }

    [Fact]
    public void ShowMainMenu_Success()
    {
        _gameService.ShowMainMenu();

        Assert.Equal(GameState.None, _gameService.LatestGameState);
    }

}