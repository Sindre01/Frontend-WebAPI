namespace backend.Interfaces
{
    public interface IWikipediaService
    {
        Task<int?> GetTopicOccurrencesAsync(string topic);
    }
}
