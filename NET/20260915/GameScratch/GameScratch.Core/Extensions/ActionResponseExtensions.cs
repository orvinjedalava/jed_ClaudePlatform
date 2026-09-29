using System.Text;
using GameScratch.Contracts.Entities.Responses;

namespace GameScratch.Core.Extensions;

public static class ActionResponseExtensions
{
    public static string ToConsoleString(this ActionResponse response)
    {
        var sb = new StringBuilder();

        sb.AppendLine("---------------------------------");
        if (!string.IsNullOrWhiteSpace(response.LLMMessage))
        {
            sb.AppendLine(response.LLMMessage);
            sb.AppendLine();
        }

        if (!string.IsNullOrWhiteSpace(response.Message))
            sb.AppendLine(response.Message);
        
        if (response.Attack != null)
        {
            sb.AppendLine();
            sb.AppendLine(response.Attack.ToConsoleString());
        }

        return sb.ToString();
    }
}