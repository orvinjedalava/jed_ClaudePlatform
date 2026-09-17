using System.Text;

namespace GameScratch.Core.Common.Responses;

public class PlayerOptionsResponse
{
    public required List<PlayerOption> Options { get; set; }

    public string ToConsoleString()
    {
        StringBuilder sb = new();
        sb.AppendLine("---------------------------------");
        sb.AppendLine("Please choose an option:");

        foreach(PlayerOption option in Options)
        {
            sb.AppendLine($"[{option.Key}]: {option.Description}");
        }

        return sb.ToString();
    }
}

public class PlayerOption
{
    public required char Key { get; init; }
    public required string ActionName { get; init; }
    public required string ServiceName { get; init; }
    public required string Description { get; init; }
    public required bool ContinueState { get; init; }
}