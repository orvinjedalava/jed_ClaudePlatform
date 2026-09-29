using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GameScratch.Core.Extensions;

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