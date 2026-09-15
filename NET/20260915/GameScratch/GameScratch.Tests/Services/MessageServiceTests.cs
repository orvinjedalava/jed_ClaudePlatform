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
    public void SendMessage_Success()
    {
        string expectedResponse = "Mocked Response";
        string message = "My message";

        _llmServiceMock
            .Setup(m => m.SendMessage(message))
            .Returns(expectedResponse);

        var result = _messageService.SendMessageToLLM(message);

        Assert.Equal(expectedResponse, result);
    }
}