using Microsoft.AspNetCore.Mvc;
using backend.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class WikipediaController : ControllerBase
{
    private readonly IWikipediaService _wikipediaService;

    public WikipediaController(IWikipediaService wikipediaService)
    {
        _wikipediaService = wikipediaService;
    }

    [HttpGet("{topic}")]
    public async Task<IActionResult> GetTopicOccurrences(string topic)
    {
        var topicCount = await _wikipediaService.GetTopicOccurrencesAsync(topic);

        if (topicCount == null)
        {
            return NotFound(new { message = "Topic text could not be retrieved or parsed." });
        }

        return Ok(new { topic = topic, count = topicCount });
    }
}


