using Microsoft.Extensions.DependencyInjection;
using GameScratch.Core.LLM;
using Microsoft.Extensions.Hosting;

namespace GameScratch.Core.Services;

public static class ServicesExtensions
{
    public static HostApplicationBuilder ConfigureCoreServices(this HostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IPlayerService, PlayerService>();
        builder.Services.AddScoped<IActionService, ActionService>();
        builder.Services.AddTransient<IDiceService, DiceService>();
        builder.Services.AddSingleton<IGameService, GameService>();
        builder.Services.AddSingleton<IInputService, InputService>();

        builder.Services.Configure<LLMServiceOptions>(builder.Configuration.GetSection("AnthropicLLMService"));
        builder.Services.AddScoped<ILLMService, LLM.Anthropic.LLMService>();
        return builder;
    }
}