namespace BlogProject.Application.Abstractions.Services
{
    public interface IAiService
    {
        Task<string> SummarizeTextAsync(string content);
    }
}
