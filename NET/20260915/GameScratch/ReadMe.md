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

# Ideas to expand AI usage
Turn it into a real multi-step agent loop, not one-shot tool choice. Right now GetToolChoiceAsync sends state and gets exactly one tool call. Add "inspection" tools (e.g., InspectOpponent, RecallLastTurns) alongside the action tools, and loop while the model keeps calling inspection tools, only finalizing when it picks an actual action tool. This is the textbook ReAct upgrade and a great story for "I converted single-shot function calling into an agentic loop."

Finish the memory story. Wire up the commented-out history logic into ILLMSessionService — persist a rolling/summarized history per GameSession (already have GameSession.cs and IGameSessionStore). Add a summarization step once history exceeds N turns (a classic "memory compaction" sub-agent).

Split into Planner/Tactician + Narrator agents. You already implicitly separate mechanics (ToolUsePicked) from flavor text (TextBlock). Formalize this as two roles: one agent purely decides the legal action (deterministic, testable), a second generates the taunt/personality text. Good talking point on separating reasoning from presentation for testability.

Add a validation/critic pass. Before executing ToolUsePicked, validate it's actually in gameResponse.PlayerOptions.Options; if not, retry with a corrective message instead of indexing Name[0] blindly (this is also a latent bug/robustness gap worth fixing regardless).

Add resilience. Wrap _client.Messages.Create with retry/backoff (Polly) for transient Anthropic API failures — ties directly into "how do you make agents production-resilient."

Add an eval harness in GameScratch.Tests. Simulate N matches against the random-fallback opponent and score win rate / rule violations / invalid tool picks over time — this is your answer to "how do you test non-deterministic agents."

Add tracing. Log each turn's prompt, chosen tool, token usage, and latency (even just structured logging first) — sets up the "observability" answer with a real example.

Optional stretch: human-in-the-loop coach. A second agent that watches the human Challenger's state and offers suggested actions/commentary — demonstrates hand-off/orchestration between agents with different roles (advisor vs. actor).