using Anthropic;
using Anthropic.Models.Messages;

using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.local.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

var apiKey = config["ANTHROPIC_API_KEY"] ?? 
    throw new InvalidOperationException("ANTHROPIC_API_KEY not configured.");

var client = new AnthropicClient(new Anthropic.Core.ClientOptions { ApiKey = apiKey });

var message = await client.Messages.Create(
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

foreach(ContentBlock block in message.Content)
{
    if (block.TryPickText(out TextBlock? textBlock))
    {
        Console.WriteLine(textBlock.Text);
    }
}
