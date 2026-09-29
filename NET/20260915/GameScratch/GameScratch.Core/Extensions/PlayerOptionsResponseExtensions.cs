using System.Text;
using GameScratch.Contracts.Entities.Responses;

namespace GameScratch.Core.Extensions;

public static class PlayerOptionsResponseExtensions
{
    public static string ToConsoleString(this PlayerOptionsResponse response)
    {
        StringBuilder sb = new();
        sb.AppendLine("---------------------------------");
        sb.AppendLine("Please choose an option:");
        sb.AppendLine();

        foreach(PlayerOption option in response.Options)
        {
            sb.AppendLine($"[{option.Key}]: {option.Description}");
        }

        return sb.ToString();
    }
}