using GameScratch.Core.LLM;

namespace GameScratch.Tests.LLM;

public class LLMServiceBaseTests
{
    private ILLMService _llmServiceBase;

    public LLMServiceBaseTests()
    {
        _llmServiceBase = new LLMServiceBase();
    }

    [Fact]
    public void ClearChatHistory_Success()
    {
        _llmServiceBase.ClearChatHistory();

        Assert.Empty(_llmServiceBase.ChatHistory);
    }
}