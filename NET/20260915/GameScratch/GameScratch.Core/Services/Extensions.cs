using Microsoft.Extensions.DependencyInjection;
using GameScratch.Core.LLM;
using Microsoft.Extensions.Hosting;
using GameScratch.Core.LLM.Anthropic;

namespace GameScratch.Core.Services;

public static class ServicesExtensions
{
    public static IHostApplicationBuilder ConfigureCoreServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IPlayerService, PlayerService>();
        builder.Services.AddScoped<IActionService, ActionService>();
        builder.Services.AddTransient<IDiceService, DiceService>();
        builder.Services.AddScoped<IGameService, GameService>();
        builder.Services.AddScoped<IInputService, InputService>();

        builder.ConfigureAnthropicServices();
        return builder;
    }
}