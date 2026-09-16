using GameScratch.Core.LLM;
using GameScratch.Core.Services;
using Moq;

namespace GameScratch.Tests.LLM;

public class LLMServiceBaseTests
{
    private ILLMService _llmServiceBase;

    private Mock<IActionService> _actionServiceMock;

    public LLMServiceBaseTests()
    {
        _actionServiceMock = new Mock<IActionService>();

        _llmServiceBase = new LLMServiceBase(_actionServiceMock.Object);
    }

    [Fact]
    public void ClearChatHistory_Success()
    {
        _llmServiceBase.ClearChatHistory();

        Assert.Empty(_llmServiceBase.ChatHistory);
    }
}