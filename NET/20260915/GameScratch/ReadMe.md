# Instructions on how to setup this solution

## Create the folder structure and solution
- cd /Users/orvinjedalava/Github/jed_ClaudePlatform/NET/20260915
- mkdir GameScratch
- cd GameScratch
- dotnet new sln -n GameScratch

## Create the projects
- dotnet new console -n GameScratch.ConsoleApp -o GameScratch.ConsoleApp
- dotnet new classlib -n GameScratch.Core -o GameScratch.Core
- dotnet new xunit -n GameScratch.Tests -o GameScratch.Tests
- dotnet new webapi -n GameScratch.MinimalApi -o GameScratch.MinimalApi
- dotnet new classlib -n GameScratch.Contracts -o GameScratch.Contracts
- dotnet new console -n GameScratch.ConsoleWebApp -o GameScratch.ConsoleWebApp

## All the three projects to the solution
- dotnet sln GameScratch.slnx add GameScratch.ConsoleApp/GameScratch.ConsoleApp.csproj
- dotnet sln GameScratch.slnx add GameScratch.Core/GameScratch.Core.csproj
- dotnet sln GameScratch.slnx add GameScratch.Tests/GameScratch.Tests.csproj
- dotnet sln GameScratch.slnx add GameScratch.MinimalApi/GameScratch.MinimalApi.csproj
- dotnet sln GameScratch.slnx add GameScratch.Contracts/GameScratch.Contracts.csproj
- dotnet sln GameScratch.slnx add GameScratch.ConsoleWebApp/GameScratch.ConsoleWebApp.csproj

## Wire up project references
- dotnet add GameScratch.ConsoleApp/GameScratch.ConsoleApp.csproj reference GameScratch.Core/GameScratch.Core.csproj
- dotnet add GameScratch.Tests/GameScratch.Tests.csproj reference GameScratch.Core/GameScratch.Core.csproj
- dotnet add GameScratch.MinimalApi/GameScratch.MinimalApi.csproj reference GameScratch.Core/GameScratch.Core.csproj
- dotnet add GameScratch.MinimalApi/GameScratch.MinimalApi.csproj reference GameScratch.Contracts/GameScratch.Contracts.csproj
- dotnet add GameScratch.ConsoleApp/GameScratch.ConsoleApp.csproj reference GameScratch.Contracts/GameScratch.Contracts.csproj
- dotnet add GameScratch.Core/GameScratch.Core.csproj reference GameScratch.Contracts/GameScratch.Contracts.csproj
- dotnet add GameScratch.ConsoleWebApp/GameScratch.ConsoleWebApp.csproj reference GameScratch.Contracts/GameScratch.Contracts.csproj

# Add dotnet package for Generic Host and other configuration packages
- dotnet add GameScratch.ConsoleApp/GameScratch.ConsoleApp.csproj package Microsoft.Extensions.Hosting
- dotnet add GameScratch.ConsoleApp/GameScratch.ConsoleApp.csproj package Microsoft.Extensions.Options
- dotnet add GameScratch.Core/GameScratch.Core.csproj package Microsoft.Extensions.Hosting
- dotnet add GameScratch.Core/GameScratch.Core.csproj package Microsoft.Extensions.Options
- dotnet add GameScratch.Tests/GameScratch.Tests.csproj package Microsoft.Extensions.Options
- dotnet add GameScratch.ConsoleApp/GameScratch.ConsoleApp.csproj package Microsoft.Extensions.Http
- dotnet add GameScratch.Contracts/GameScratch.Contracts.csproj package Microsoft.Extensions.Caching.Abstractions
- dotnet add GameScratch.ConsoleWebApp/GameScratch.ConsoleWebApp.csproj package Microsoft.Extensions.Http

# Add Swagger UI NuGet package
- dotnet add GameScratch.MinimalApi/GameScratch.MinimalApi.csproj package Scalar.AspNetCore

# Add Anthropic package
- dotnet add GameScratch.Core/GameScratch.Core.csproj package Anthropic