using System.Text.Json;
using backend.Interfaces;
using backend.Extensions;
using HtmlAgilityPack;

namespace backend.Services
{
    public class WikipediaService : IWikipediaService
    {
        public async Task<int?> GetTopicOccurrencesAsync(string topic)
        {
            var url = $"https://en.wikipedia.org/w/api.php?action=parse&section=0&prop=text&format=json&page={topic}";

            using var client = new HttpClient();
            var response = await client.GetStringAsync(url);
            var jsonData = JsonSerializer.Deserialize<JsonElement>(response);


            //Could not find page
            if (jsonData.TryGetProperty("error", out var errorProperty))
            {
                return 0;
            }

            var htmlContent = jsonData.GetProperty("parse").GetProperty("text").GetProperty("*").GetString();

            //HtmlAgilityPack to se text of each html element
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlContent);
            var textContent = doc.DocumentNode.InnerText;
            return StringExtensions.CountOccurrences(textContent, topic);
        }
    }
}
