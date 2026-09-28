using GameScratch.ConsoleApp;
using GameScratch.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GameScratch.Core.Common.Responses;
using GameScratch.Contracts.Enums;

Console.WriteLine("---------------------------------");
Console.WriteLine("Gladiator Fight!");

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.local.json", optional: true)
    .AddEnvironmentVariables();

builder.ConfigureServices();

using IHost host = builder.Build();

var httpClientFactory = host.Services.GetRequiredService<IHttpClientFactory>();
var weatherForecast = await GetWeatherForecastAsync(httpClientFactory);
Console.WriteLine($"Weather Forecast: {weatherForecast}");


var gameService = host.Services.GetRequiredService<IGameService>();

// Console.WriteLine(await messageService.SendMessageToLLMAsync("What should I search for to find the latest developments in renewable energy?"));

await EnterMainMenu();

async Task EnterMainMenu()
{
    // Console.Clear();
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
            if (response.GameTurn == GameTurn.ChallengerTurn || response.GameTurn == GameTurn.ChampionTurn)
            {
                await EnterMatch(response.GameTurn, response.GameMode);
            }
        }
        else
        {
            break;
        }
    }
}

async Task EnterMatch(GameTurn gameState, GameMode gameMode)
{
    GameResponse gameResponse;
    while(true)
    {
        char inputChar = '\0';
        if (gameState == GameTurn.ChallengerTurn || gameMode == GameMode.TwoPlayers)
        {
            inputChar = Console.IsInputRedirected ? 
                (Console.ReadLine()?.FirstOrDefault() ?? '\0')
                : Console.ReadKey(true).KeyChar;
            
            gameResponse =  gameService.HandleInput(inputChar);
        }
        else
        {
            Console.WriteLine($"The champion is about to make a move...");
            gameResponse = await gameService.ExecuteChampionTurnAsync();
        }
        
        Console.Clear();
        Console.WriteLine(gameResponse.ToConsoleString());
        Console.WriteLine("\n");

        if (!gameResponse.ContinueState)
            break;

        gameState = gameResponse.GameTurn;
    }
}

async Task<string> GetWeatherForecastAsync(IHttpClientFactory clientFactory)
{
    try
    {
        var client = clientFactory.CreateClient("GameApi");
        var response = await client.GetAsync("/weatherforecast");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        else
        {
            return $"Error: {response.StatusCode}";
        }
    }
    catch(Exception ex)
    {
        return $"Exception: {ex.Message}";
    }
}

