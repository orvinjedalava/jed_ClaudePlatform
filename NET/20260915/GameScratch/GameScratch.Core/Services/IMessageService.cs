namespace GameScratch.Core.Services;

public interface IMessageService
{
    string SendMessageToLLM(string message);
}