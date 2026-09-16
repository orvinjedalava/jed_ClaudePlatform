using Microsoft.Extensions.DependencyInjection;
using GameScratch.Core.LLM;
using GameScratch.Core.Services;
using Microsoft.Extensions.Hosting;

namespace GameScratch.Core.Services;

public static class ServicesExtensions
{
    public static HostApplicationBuilder ConfigureCoreServices(this HostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IPlayerService, PlayerService>();
        builder.Services.AddScoped<IActionService, ActionService>();
        builder.Services.AddSingleton<IGameService, GameService>();

        builder.Services.Configure<LLMServiceOptions>(builder.Configuration.GetSection("AnthropicLLMService"));
        builder.Services.AddScoped<ILLMService, LLM.Anthropic.LLMService>();
        return builder;
    }
}