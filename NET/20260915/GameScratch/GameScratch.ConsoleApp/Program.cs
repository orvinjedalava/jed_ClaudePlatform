using GameScratch.ConsoleApp;
using GameScratch.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Hello, World!");

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.local.json", optional: true)
    .AddEnvironmentVariables();

builder.ConfigureServices();

using IHost host = builder.Build();

var messageService = host.Services.GetRequiredService<IMessageService>();

Console.WriteLine(await messageService.SendMessageToLLMAsync("What should I search for to find the latest developments in renewable energy?"));