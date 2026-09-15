namespace GameScratch.Core.LLM;

public interface ILLMService
{
    Task<string> SendMessageAsync(string message);
}