using GameScratch.ConsoleApp;
using GameScratch.Core.Services;
using GameScratch.Core.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("---------------------------------");
Console.WriteLine("Gladiator Fight!");

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.local.json", optional: true)
    .AddEnvironmentVariables();

builder.ConfigureServices();

using IHost host = builder.Build();

var gameService = host.Services.GetRequiredService<IGameService>();

// Console.WriteLine(await messageService.SendMessageToLLMAsync("What should I search for to find the latest developments in renewable energy?"));

await EnterMainMenu();

async Task EnterMainMenu()
{
    Console.Clear();
    Console.WriteLine(gameService.ShowMainMenu().ToConsoleString());

    while(true)
    {
        char inputChar = Console.IsInputRedirected ? 
            (Console.ReadLine()?.FirstOrDefault() ?? '\0')
            : Console.ReadKey(true).KeyChar;

        var response = gameService.HandleInput(inputChar);

        Console.Clear();
        Console.WriteLine(response.ToConsoleString());
        Console.WriteLine("\n");

        if (response.ContinueState)
        {
            if (response.GameState == GameState.ChallengerTurn)
            {
                await EnterMatch();
            }
        }
        else
        {
            break;
        }
    }
}

async Task EnterMatch()
{
    while(true)
    {
        char inputChar = Console.IsInputRedirected ? 
            (Console.ReadLine()?.FirstOrDefault() ?? '\0')
            : Console.ReadKey(true).KeyChar;

        var response = gameService.HandleInput(inputChar);

        Console.Clear();
        Console.WriteLine(response.ToConsoleString());
        Console.WriteLine("\n");

        if (!response.ContinueState)
            break;
    }
}

