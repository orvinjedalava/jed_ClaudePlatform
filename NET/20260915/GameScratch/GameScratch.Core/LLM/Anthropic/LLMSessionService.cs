using Anthropic;
using Anthropic.Core;
using Anthropic.Models.Messages;

namespace GameScratch.Core.LLM.Anthropic;

public class LLMSessionService : ILLMSessionService
{
    public IReadOnlyList<ContentBlock>? ToolChoiceContent { get; set; }
    public ToolUseBlock? ToolUsePicked { get; set; }
    public string? UserPrompt { get; set; }
    public List<ToolUnion>? Tools { get; set; }
    public ToolChoice? ToolChoice { get;set; }
}