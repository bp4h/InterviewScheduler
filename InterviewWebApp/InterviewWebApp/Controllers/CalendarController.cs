using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
    public async Task<IActionResult> GenerateLink([FromQuery] string guid)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var link = await _calendarService.GenerateLinkAsync(userId, guid);
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
    [HttpGet("interviewsByCode")]
    public async Task<IActionResult> GetInterviewsByCode([FromQuery] string code)
    {
        var interviews = await _calendarService.GetInterviewsByCodeAsync("2823A6C3-BE29-4C2B-87B5-9FE489974650");

        if (interviews == null)
        {
            return NotFound("Link not found or inactive for the provided code.");
        }

        var simpleInterviews = interviews.Select(i => new
        {
            Start = i.Start,
            End = i.End,
            Title = i.Title
        }).ToArray();

        return Ok(simpleInterviews);
    }
}
