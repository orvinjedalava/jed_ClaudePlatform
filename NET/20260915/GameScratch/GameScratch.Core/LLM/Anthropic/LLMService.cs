
using Microsoft.Extensions.Options;

using Anthropic;
using Anthropic.Core;
using Anthropic.Models.Messages;
using System.Text;

using GameScratch.Core.Services;
using GameScratch.Core.Common.Players;

namespace GameScratch.Core.LLM.Anthropic;

public class LLMService : LLMServiceBase, ILLMService
{
    private readonly LLMServiceOptions _options;
    private readonly AnthropicClient _client;

    public LLMService(IOptions<LLMServiceOptions> options, IActionService actionService)
        : base(actionService)
    {
        _options = options?.Value ?? throw new ArgumentNullException("LLMServiceOptions not dependency injected.");
        _client = new AnthropicClient(new ClientOptions { ApiKey = _options.ApiKey });

    }

    public new async Task<string> ExecuteTurnAsync(Player champion, Player challenger)
    {
        if (!_options.Enabled)
            return await base.ExecuteTurnAsync(champion, challenger);
        
        return await SendMessageAsync("TODO message");
    }
    
    public new async Task<string> SendMessageAsync(string message)
    {
        if (!_options.Enabled)
            return "LLM is not enabled.";
        
        Message responseMsg = await _client.Messages.Create(
            new MessageCreateParams()
            {
                Model = Model.ClaudeHaiku4_5_20251001,
                MaxTokens = 1000,
                Messages =
                [
                    new MessageParam()
                    {
                        Role = Role.User,
                        Content = new MessageParamContent(
                            [
                                new ContentBlockParam(
                                    new TextBlockParam("What should I search for to find the latest developments in renewable energy?"))
                            ]
                        )
                    }
                ]
            }
        );

        var sb = new StringBuilder();

        foreach(ContentBlock block in responseMsg.Content)
        {
            if (block.TryPickText(out TextBlock? textBlock))
            {
                sb.AppendLine(textBlock.Text);
            }
        }

        return sb.ToString();
    }
}