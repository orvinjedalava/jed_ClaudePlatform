using Anthropic;
using Anthropic.Core;
using Anthropic.Models.Messages;

namespace GameScratch.Core.LLM.Anthropic;

public class LLMSessionService : ILLMSessionService
{
    public IReadOnlyList<ContentBlock>? ToolChoiceContent { get; set; }
}