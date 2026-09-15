using Moq;

using GameScratch.Core.Services;
using GameScratch.Core.LLM;

namespace GameScratch.Tests.Services;

public class GameServiceTests
{
    private readonly IGameService _gameService;
    private readonly Mock<ILLMService> _llmServiceMock;

    public GameServiceTests()
    {
        // initialize mocks
        _llmServiceMock = new Mock<ILLMService>();

        _gameService = new GameService(
            llmService: _llmServiceMock.Object
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
}