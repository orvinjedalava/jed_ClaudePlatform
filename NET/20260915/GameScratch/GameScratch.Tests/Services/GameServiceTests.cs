using Moq;

using GameScratch.Core.Services;
using GameScratch.Core.LLM;
using GameScratch.Core.Common;

namespace GameScratch.Tests.Services;

public class GameServiceTests
{
    private readonly IGameService _gameService;
    private readonly Mock<ILLMService> _llmServiceMock;
    private readonly Mock<IPlayerService> _playerServiceMock;

    public GameServiceTests()
    {
        // initialize mocks
        _llmServiceMock = new Mock<ILLMService>();
        _playerServiceMock = new Mock<IPlayerService>();

        _gameService = new GameService(
            llmService: _llmServiceMock.Object,
            playerService: _playerServiceMock.Object
        );
    }

    [Fact]
    public async Task SendMessageToLLMAsync_Success()
    {
        string expectedResponse = "Mocked Response";
        string message = "My message";

        _llmServiceMock
            .Setup(m => m.SendMessageAsync(message))
            .ReturnsAsync(expectedResponse);

        var result = await _gameService.SendMessageToLLMAsync(message);

        Assert.Equal(expectedResponse, result);
    }

    [Fact]
    public void Reset_Success()
    {
        var exception = Record.Exception(() => _gameService.Reset());

        Assert.Null(exception);
    }

    [Fact]
    public void Start_Success()
    {
        var exception = Record.Exception(() => _gameService.Start());

        Assert.Null(exception);
    }

    [Theory]
    [InlineData('1', GameState.ChallengerTurn, true)]
    [InlineData('2', GameState.ChallengerTurn, true)]
    [InlineData('s', GameState.None, true)]
    public void HandleInput_Success(char keyChar, GameState gameState, bool expectedContinueGame)
    {
        var gameService = new GameService(
            llmService: _llmServiceMock.Object,
            playerService: _playerServiceMock.Object
        )
        { 
            LatestGameState = gameState 
        };

        (bool isContinue, _) = gameService.HandleChallengerInput(keyChar);

        Assert.Equal(expectedContinueGame, isContinue);
    }

    [Theory]
    [InlineData('1', true)]
    [InlineData('2', true)]
    [InlineData('r', true)]
    [InlineData('q', false)]
    [InlineData('o', true)]
    [InlineData('9', true)]
    public void HandleChallengerInput_Success(char keyChar, bool expectedContinueGame)
    {
        (bool isContinue, _) = _gameService.HandleChallengerInput(keyChar);

        Assert.Equal(expectedContinueGame, isContinue);
    }

    [Theory]
    [InlineData('s', true)]
    [InlineData('1', true)]
    [InlineData('r', true)]
    [InlineData('c', false)]
    public void HandleGameStateNoneInput_Success(char keyChar, bool expectedContinueGame)
    {
        (bool isContinue, _) = _gameService.HandleGameStateNoneInput(keyChar);

        Assert.Equal(expectedContinueGame, isContinue);
    }

    [Fact]
    public async Task ExecuteChampionTurn_Success()
    {
       await _gameService.ExecuteChampionTurnAsync();

       Assert.NotEqual(GameState.ChampionTurn, _gameService.LatestGameState); 
    }

    [Fact]
    public void ShowMainMenu_Success()
    {
        _gameService.ShowMainMenu();

        Assert.Equal(GameState.None, _gameService.LatestGameState);
    }

}