using Moq;

using GameScratch.Core.Services;
using GameScratch.Core.LLM;

namespace GameScratch.Tests.Services;

public class MessageServiceTests
{
    private readonly IMessageService _messageService;
    private readonly Mock<ILLMService> _llmServiceMock;

    public MessageServiceTests()
    {
        // initialize mocks
        _llmServiceMock = new Mock<ILLMService>();

        _messageService = new MessageService(
            llmService: _llmServiceMock.Object
        );
    }

    [Fact]
    public async Task SendMessage_Success()
    {
        string expectedResponse = "Mocked Response";
        string message = "My message";

        _llmServiceMock
            .Setup(m => m.SendMessageAsync(message))
            .ReturnsAsync(expectedResponse);

        var result = await _messageService.SendMessageToLLMAsync(message);

        Assert.Equal(expectedResponse, result);
    }
}