using Microsoft.Extensions.DependencyInjection;
using GameScratch.Core.LLM;
using System.Text;
using GameScratch.Core.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Http;
using GameScratch.Core.Common.Responses;

namespace GameScratch.ConsoleApp;

public static class Extensions
{
    public static HostApplicationBuilder ConfigureServices(this HostApplicationBuilder builder)
    {
        builder.Services.AddHttpClient("GameApi", client =>
        {
            client.BaseAddress = new Uri("http://localhost:5000");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        builder.ConfigureCoreServices();
        return builder;
    }
}