using Microsoft.Extensions.DependencyInjection;
using GameScratch.Core.LLM;
using System.Text;
using GameScratch.Core.Services;
using Microsoft.Extensions.Hosting;
using GameScratch.Core.Common.Responses;

namespace GameScratch.ConsoleApp;

public static class Extensions
{
    public static HostApplicationBuilder ConfigureServices(this HostApplicationBuilder builder)
    {
        builder.ConfigureCoreServices();
        return builder;
    }
}