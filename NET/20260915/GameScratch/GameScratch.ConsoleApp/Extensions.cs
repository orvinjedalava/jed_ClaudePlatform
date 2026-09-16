using Microsoft.Extensions.DependencyInjection;
using GameScratch.Core.LLM;
using GameScratch.Core.Services;
using Microsoft.Extensions.Hosting;

namespace GameScratch.ConsoleApp;

public static class Extensions
{
    public static HostApplicationBuilder ConfigureServices(this HostApplicationBuilder builder)
    {
        builder.ConfigureCoreServices();
        return builder;
    }
}