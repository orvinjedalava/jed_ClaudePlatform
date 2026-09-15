using Microsoft.Extensions.Configuration;

Console.WriteLine("Hello, World!");

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.local.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

var apiKey = config["ANTHROPIC_API_KEY"] ??
    throw new InvalidOperationException("ANTHROPIC_API_KEY not configured.");
