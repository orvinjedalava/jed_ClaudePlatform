using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace GameScratch.Core.LLM.Anthropic;

public static class AnthropicExtensions
{
    public static IHostApplicationBuilder ConfigureAnthropicServices(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<LLMServiceOptions>(builder.Configuration.GetSection("AnthropicLLMService"));
        
        builder.Services.AddScoped<ILLMService, LLMService>();
        builder.Services.AddSingleton<ILLMSessionService, LLMSessionService>();

        return builder;
    }
}