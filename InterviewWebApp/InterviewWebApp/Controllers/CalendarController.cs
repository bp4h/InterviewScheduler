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
}
