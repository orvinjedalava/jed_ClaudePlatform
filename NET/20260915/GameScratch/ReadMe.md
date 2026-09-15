# Instructions on how to setup this solution

## Create the folder structure and solution
- cd /Users/orvinjedalava/Github/jed_ClaudePlatform/NET/20260915
- mkdir GameScratch
- cd GameScratch
- dotnet new sln -n GameScratch

## Create the three projects
- dotnet new console -n GameScratch.ConsoleApp -o GameScratch.ConsoleApp
- dotnet new classlib -n GameScratch.Core -o GameScratch.Core
- dotnet new xunit -n GameScratch.Tests -o GameScratch.Tests

## All the three projects to the solution
- dotnet sln GameScratch.slnx add GameScratch.ConsoleApp/GameScratch.App.csproj
- dotnet sln GameScratch.slnx add GameScratch.Core/GameScratch.Core.csproj
- dotnet sln GameScratch.slnx add GameScratch.Tests/GameScratch.Tests.csproj

## Wire up project references
- dotnet add GameScratch.App/GameScratch.ConsoleApp.csproj reference GameScratch.Core/GameScratch.Core.csproj
- dotnet add GameScratch.Tests/GameScratch.Tests.csproj reference GameScratch.Core/GameScratch.Core.csproj