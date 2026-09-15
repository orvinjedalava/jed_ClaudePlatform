using Microsoft.Extensions.DependencyInjection;
using GameScratch.Core.LLM;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace GameScratch.ConsoleApp;

public static class Extensions
{
    public static HostApplicationBuilder ConfigureServices(this HostApplicationBuilder builder)
    {
        IConfigurationSection anthropicSection = builder.Configuration.GetSection("AnthropicLLMService");
        builder.Services.Configure<LLMServiceOptions>(builder.Configuration.GetSection("AnthropicLLMService"));
        builder.Services.AddScoped<ILLMService, Core.LLM.Anthropic.LLMService>();
        return builder;
    }
}