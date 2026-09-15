namespace GameScratch.Core.Services;

public interface IMessageService
{
    Task<string> SendMessageToLLMAsync(string message);
}