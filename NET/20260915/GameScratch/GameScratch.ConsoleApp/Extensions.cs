using Microsoft.Extensions.DependencyInjection;
using GameScratch.Core.LLM;
using GameScratch.Core.Services;
using Microsoft.Extensions.Hosting;

namespace GameScratch.ConsoleApp;

public static class Extensions
{
    public static HostApplicationBuilder ConfigureServices(this HostApplicationBuilder builder)
    {
        builder.Services.Configure<LLMServiceOptions>(builder.Configuration.GetSection("AnthropicLLMService"));
        builder.Services.AddScoped<IGameService, GameService>();
        builder.Services.AddScoped<ILLMService, Core.LLM.Anthropic.LLMService>();
        return builder;
    }
}