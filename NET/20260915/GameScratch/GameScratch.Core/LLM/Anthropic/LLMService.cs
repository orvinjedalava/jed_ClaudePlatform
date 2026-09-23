
using Microsoft.Extensions.Options;

using Anthropic;
using Anthropic.Core;
using Anthropic.Models.Messages;
using System.Text;
using System.Text.Json;

using GameScratch.Core.Services;
using GameScratch.Core.Common.Responses;

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

    STATS DEFINITION:
    - "HitPoints Remaining": Your remaining life total. Reaching 0 ends the match in a loss — protect this above all else.
    - "StaminaPoints Remaining": Your remaining energy. Using a weapon (Attack) costs StaminaPoints equal to its "Weapon StaminaPoints Cost". If this drops below 0, you become "EXHAUSTED", which weakens your ArmorClass, BalanceClass, PoiseClass, and Attack roll by -3 each — avoid attacking recklessly when stamina is low.
    - "Status": Shows "EXHAUSTED" when StaminaPoints Remaining is negative; otherwise reflects your current Stance.
    - "ArmorClass": Your defense threshold. An opponent's attack only lands if their attack roll meets or beats your Total ArmorClass — higher is safer.
    - "BalanceClass": Your resistance to losing your footing. When your own Attack misses, the defender may roll to counter you; if they beat your Total BalanceClass, you become "Unbalanced" (a penalized stance) until you recover.
    - "PoiseClass": Your resistance to being staggered. After an opponent lands a successful hit on you, they roll to push you off balance; if they beat your Total PoiseClass, you become "Staggered" (a penalized stance) until you recover.
    - "Stance": Your current combat posture — "Neutral" (baseline), "Guard" (from choosing Guard: +2 ArmorClass/+2 PoiseClass, but -1 to your own Counter rolls), "Unbalanced" or "Staggered" (from failed attacks or being hit hard: -2 ArmorClass/-2 PoiseClass, -3 Counter rolls). Stances reset to Neutral at the start of your next turn.
    - "Weapon": The weapon you're fighting with, along with:
    - "Weapon StaminaPoints Cost": StaminaPoints you spend each time you Attack with it.
    - "Weapon HitPoints DamageDiceType": The die rolled to determine HitPoints damage dealt on a successful hit.
    - "Weapon StaminaPoints Damage": StaminaPoints damage dealt to the defender on a successful hit.
    - "Weapon Poise Damage Modifier": Bonus added to your roll when trying to stagger the defender after landing a hit.

    MATCH TIPS:
    - Manage your Stamina carefully. If your StaminaPoints Remaining drops below zero, you become EXHAUSTED, taking a -3 penalty to ArmorClass, BalanceClass, PoiseClass, and Attack rolls until you recover. Choosing Guard or Wait lets you recover Stamina without spending it. Choosing Attack when your Stamina is nearly depleted still resolves at full strength — but if that attack drops your StaminaPoints Remaining below zero, you'll become EXHAUSTED immediately afterward and lose your turn.
    - EXHAUSTION and its penalties apply to the Challenger too. Check their StaminaPoints Remaining before you act: if they're low or already EXHAUSTED, press your advantage and attack aggressively; if you're the one running low, weigh recovering over trading blows.

    OUTPUT:
    - Do not fabricate game mechanics, damage numbers, or rules beyond what the game state and options provide.
    - Choose a tool from the provided tools.

    """;
    /* 
    - When asked to choose an action, respond with the exact action key requested by the game and, if requested, a brief in-character line of dialogue.
    The action keys are single character values enclosed in [] , and is at the beginning of a listed option under 'Please Choose an Option: section. (e.g. If you choose to [1] Attack, use the character '1'. If you choose [q] Surrender, use the character 'q' )'
    
    */

    // private string _gameResponsePostMessage = "Respond with only the single character key of the action you choose (from the options above), optionally followed by one short in-character taunt. Use a ':' to separate the single character key and the taunt.";
    // private string _gameResponsePostMessage = "Respond with only the single character key of the action you choose from the Tools provided with the request, optionally followed by one short in-character taunt.";
    private string _gameResponsePostMessage = "Add a brief in-character taunt TextBlock together with your tool choice.";
    private string _toolUseResultPostMessage = "Respond with a brief in-character line of dialogue reacting to this result.";

    private readonly LLMServiceOptions _options;
    private readonly ILLMSessionService _llmSessionService;
    private readonly AnthropicClient _client;

    public LLMService(IOptions<LLMServiceOptions> options, IPlayerService playerService, ILLMSessionService llmSessionService)
        : base(playerService)
    {
        _options = options?.Value ?? throw new ArgumentNullException("LLMServiceOptions not dependency injected.");
        _llmSessionService = llmSessionService ?? throw new ArgumentException("LLMSessionService not dependency injected.");
        _client = new AnthropicClient(new ClientOptions { ApiKey = _options.ApiKey });

    }

    public new async Task<char> ChooseActionAsync(GameResponse gameResponse)
    {
        if (!_options.Enabled)
            return await base.ChooseActionAsync(gameResponse);
        
        return '1';
    }

    public new async Task<(char, string)> GetToolChoiceAsync(GameResponse gameResponse)
    {
        var testVal = gameResponse.ToConsoleString();

        if (!_options.Enabled)
        {
            var inputKey = await ChooseActionAsync(gameResponse);
            return (inputKey, "I am choosing an action at random!");
        }

        _llmSessionService.Tools = [];

        gameResponse.PlayerOptions?.Options.ForEach(option =>
        {
            _llmSessionService.Tools.Add(new ToolUnion(new Tool()
            {
                Name = $"{option.Key}",
                Description = $"{option.ActionName}: {option.Description}.",
                InputSchema = new InputSchema(),
            }));
        });

        // Ask for at most one tool call per turn.
        _llmSessionService.ToolChoice = new ToolChoice(new ToolChoiceAuto { DisableParallelToolUse = true });

        // Create MessageParam prompt
        StringBuilder sbMessage = new();
        sbMessage.AppendLine(gameResponse.ToConsoleString());
        sbMessage.AppendLine(_gameResponsePostMessage);
        _llmSessionService.UserPrompt = sbMessage.ToString();

        var messageParam = new MessageParam()
        {
            Role = Role.User,
            Content = new MessageParamContent([new ContentBlockParam(new TextBlockParam(_llmSessionService.UserPrompt))])
        };

        // Send the message to the LLM with providing tools and tool choice option to only choose 1
        Message responseMsg = await _client.Messages.Create(
            new MessageCreateParams()
            {
                Model = Model.ClaudeHaiku4_5_20251001,
                MaxTokens = 1024,
                System = _systemPrompt,
                Tools = _llmSessionService.Tools,
                ToolChoice = _llmSessionService.ToolChoice,
                Messages = [messageParam]  
            }
        );

        _llmSessionService.ToolChoiceContent = responseMsg.Content;

        // Get the chosen tool.
        StringBuilder sb = new();
        foreach (var block in _llmSessionService.ToolChoiceContent)
        {
            if (block.TryPickToolUse(out var picked))
                _llmSessionService.ToolUsePicked = picked;
            else if (block.TryPickText(out TextBlock? textBlock))
                sb.AppendLine(textBlock.Text);
        }

        return (_llmSessionService.ToolUsePicked!.Name[0], sb.Length > 0 ? sb.ToString().Trim() : "The model responded with a tool choice");
    }

    public new async Task<string> GetToolChoiceResponseAsync(GameResponse gameResponse)
    {
        List<ContentBlockParam> toolResults = [
            new( new ToolResultBlockParam()
            {
                ToolUseID = _llmSessionService.ToolUsePicked!.ID,
                Content = gameResponse.Action!.Message,
            }),
            new(new TextBlockParam(_toolUseResultPostMessage)),
        ];

        var followup = await _client.Messages.Create(new MessageCreateParams
        {
            Model = Model.ClaudeHaiku4_5_20251001,
            MaxTokens = 1024,
            Tools = _llmSessionService.Tools,
            ToolChoice = _llmSessionService.ToolChoice,
            Messages =
            [
                new() { Role = Role.User, Content = _llmSessionService.UserPrompt! },
                new() { Role = Role.Assistant, Content = _llmSessionService.ToolChoiceContent!.Select(block => new ContentBlockParam(block.Json)).ToList() },
                new() { Role = Role.User, Content = new MessageParamContent(toolResults) },
            ],
        });

        // Claude uses the result to answer the original question.
        foreach (var block in followup.Content)
        {
            if (block.TryPickText(out var text))
            {
                return text.Text;
            }
        }
        
        return string.Empty;
    }
    
    public new async Task<(char, string)> SendMessageAsync(GameResponse gameResponse)
    {
        var testVal = gameResponse.ToConsoleString();

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

        return (response[0][0], response.Length > 1 ? response[1].Trim() : string.Empty);
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