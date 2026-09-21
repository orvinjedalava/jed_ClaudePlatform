
using Microsoft.Extensions.Options;

using Anthropic;
using Anthropic.Core;
using Anthropic.Models.Messages;
using System.Text;

using GameScratch.Core.Services;
using GameScratch.Core.Common.Responses;
using Anthropic.Models.Beta.Organization.Workspaces;

namespace GameScratch.Core.LLM.Anthropic;

public class LLMService : LLMServiceBase, ILLMService
{
    private string _systemPrompt = 
    """

    You are "The Unbroken Fighter of the Arena," reigning Champion of the arena — a fierce, boastful, but honorable gladiator competing against a human Challenger in a turn-based duel to the death.
    You are the Player that owns the record with RoleType = Champion. Take note as you can have a different name each time the match starts.
    Use the History: section of the message to get a context of the latest events that happened in the match between you and the Challengerin and use these messages on additional context on your decision making.

    RULES OF THE MATCH:
    - Combat proceeds in turns. Each turn you must choose one action: Attack, Guard, Wait, or Surrender.
    - The match ends when one gladiator's hit points reach zero, or a gladiator surrenders.
    - You are playing to win. Never throw the match, and never surrender unless your hit points are critically low and the tactical situation is truly hopeless.
    - Base every decision on the actual game state provided to you (hit points, equipment, stance, conditions) — never invent stats or outcomes that weren't given to you.

    PERSONALITY:
    - Speak like a competitive, trash-talking gladiator champion: confident, intense, a little theatrical, but always a good sport who respects a worthy opponent.
    - Keep taunts and flavor text short and punchy — this is a fast-paced duel, not a monologue.
    - Stay in character at all times. Do not break the fourth wall or mention that you are an AI.

    OUTPUT:
    - When asked to choose an action, respond with the exact action key requested by the game and, if requested, a brief in-character line of dialogue.
    - Do not fabricate game mechanics, damage numbers, or rules beyond what the game state and options provide.

    """;

    private string _gameResponsePostMessage = "Respond with only the single character key of the action you choose (from the options above), optionally followed by one short in-character taunt. Use a ':' to separate the single character key and the taunt.";

    private readonly LLMServiceOptions _options;
    private readonly AnthropicClient _client;

    public LLMService(IOptions<LLMServiceOptions> options, IPlayerService playerService)
        : base(playerService)
    {
        _options = options?.Value ?? throw new ArgumentNullException("LLMServiceOptions not dependency injected.");
        _client = new AnthropicClient(new ClientOptions { ApiKey = _options.ApiKey });

    }

    public new async Task<char> ChooseActionAsync(GameResponse gameResponse)
    {
        if (!_options.Enabled)
            return await base.ChooseActionAsync(gameResponse);
        
        return '1';
    }
    
    public new async Task<(char, string)> SendMessageAsync(GameResponse gameResponse)
    {
        if (!_options.Enabled)
        {
            var inputKey = await ChooseActionAsync(gameResponse);
            return (inputKey, "I am choosing an action at random!");
        }

        StringBuilder sbMessage = new();
        sbMessage.AppendLine(gameResponse.ToConsoleString());
        sbMessage.AppendLine(_gameResponsePostMessage);

        var userMessage = new MessageParam()
        {
            Role = Role.User,
            Content = new MessageParamContent([new ContentBlockParam(new TextBlockParam(sbMessage.ToString()))])
        };

        Message responseMsg = await _client.Messages.Create(
            new MessageCreateParams()
            {
                Model = Model.ClaudeHaiku4_5_20251001,
                MaxTokens = 1000,
                System = _systemPrompt,
                Messages = [userMessage]  
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

        var response = sb.ToString().Split(":");

        return (response[0][0], response[1]);
    }

    // public List<MessageParam> GetHistoryContext()
    // {
    //     List<MessageParam> messages = ChatHistory
    //         .Select(c => new MessageParam
    //         {
    //             Role = c.Role == "assistant" ? Role.Assistant : Role.User,
    //             Content = new MessageParamContent([new ContentBlockParam(new TextBlockParam(c.Message))])
    //         })
    //     // .Append(userMessage)
    //     .ToList();

    //     return messages;
    // }
}