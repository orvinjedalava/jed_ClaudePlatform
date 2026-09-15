namespace GameScratch.Core.LLM;

public interface ILLMService
{
    string SendMessage(string message);
}