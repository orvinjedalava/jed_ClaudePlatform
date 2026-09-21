using GameScratch.ConsoleApp;
using GameScratch.Core.Services;
using GameScratch.Core.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GameScrach.Core.Common;
using GameScratch.Core.Common.Responses;

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
            if (response.GameState == GameState.ChallengerTurn || response.GameState == GameState.ChampionTurn)
            {
                await EnterMatch(response.GameState, response.GameMode);
            }
        }
        else
        {
            break;
        }
    }
}

async Task EnterMatch(GameState gameState, GameMode gameMode)
{
    GameResponse gameResponse;
    while(true)
    {
        char inputChar = '\0';
        if (gameState == GameState.ChallengerTurn || gameMode == GameMode.TwoPlayers)
        {
            inputChar = Console.IsInputRedirected ? 
                (Console.ReadLine()?.FirstOrDefault() ?? '\0')
                : Console.ReadKey(true).KeyChar;
        }
        else
        {
            Console.WriteLine($"The champion is about to make a move...");
            gameResponse = await gameService.ExecuteChampionTurnAsync();
        }
        
        gameResponse =  gameService.HandleInput(inputChar);

        Console.Clear();
        Console.WriteLine(gameResponse.ToConsoleString());
        Console.WriteLine("\n");

        if (!gameResponse.ContinueState)
            break;

        gameState = gameResponse.GameState;
    }
}

