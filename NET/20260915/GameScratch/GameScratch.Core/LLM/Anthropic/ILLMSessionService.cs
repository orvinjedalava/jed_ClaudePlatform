
using Anthropic;
using Anthropic.Core;
using Anthropic.Models.Messages;

namespace GameScratch.Core.LLM.Anthropic;

public interface ILLMSessionService
{
    IReadOnlyList<ContentBlock>? ToolChoiceContent { get; set; }
}