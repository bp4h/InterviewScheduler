using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class CalendarController : ControllerBase
{
    private readonly ICalendarService _calendarService;

    public CalendarController(ICalendarService calendarService)
    {
        _calendarService = calendarService;
    }

    [HttpPost("generate-link")]
    public async Task<IActionResult> GenerateLink()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var link = await _calendarService.GenerateLinkAsync(userId);
        return Ok(new { Link = link });
    }
    [HttpGet("interviews")]
    public async Task<IActionResult> GetInterviews()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var interviews = await _calendarService.GetInterviewsAsync(userId);

        var simpleInterviews = interviews.Select(i => new
        {
            Start = i.Start,
            End = i.End,
            Title = i.Title
        }).ToArray();

        return Ok(simpleInterviews);
    }
}
