
using Microsoft.Extensions.Options;

namespace GameScratch.Core.LLM.Anthropic;

public class LLMService(IOptions<LLMServiceOptions> options) : ILLMService
{
    private readonly LLMServiceOptions _options = options?.Value ?? 
        throw new ArgumentNullException("LLMServiceOptions not dependency injected.");

    string ILLMService.SendMessage(string message)
    {
        return $"LLMService is using model {_options.ModelName}";
    }
}