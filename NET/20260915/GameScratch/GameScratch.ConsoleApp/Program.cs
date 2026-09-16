using GameScratch.ConsoleApp;
using GameScratch.Core.Services;
using GameScratch.Core.Common;
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

var gameService = host.Services.GetRequiredService<IGameService>();

// Console.WriteLine(await messageService.SendMessageToLLMAsync("What should I search for to find the latest developments in renewable energy?"));

EnterMainMenu();

void EnterMainMenu()
{
    bool inMainMenu = true;

    var response = gameService.ShowMainMenu();
    Console.WriteLine(response);

    while(inMainMenu)
    {
        
        char inputChar = Console.IsInputRedirected ? 
            (Console.ReadLine()?.FirstOrDefault() ?? '\0')
            : Console.ReadKey(true).KeyChar;

        (bool isContinue, string responseMsg) = gameService.HandleInput(inputChar);
        Console.WriteLine("\n");

        Console.WriteLine(responseMsg);

        if (isContinue)
        {
            // (bool isContinueMatch, string responseMatchMsg) = gameService.HandleGameStateNoneInput(key.KeyChar);

            // if (gameService.LatestGameState != GameState.None)
            // {
            //     bool matchInProgress = true;

            //     while(matchInProgress)
            //     {
                    

            //         if (gameService.LatestGameState == GameState.None)
            //         {
            //             matchInProgress = false;
            //         }
            //     }
            // }
        }
        else
        {
            inMainMenu = false;

        }

    }
}

