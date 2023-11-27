using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class CalendarController : ControllerBase
{
    private readonly ICalendarService _calendarService;
    private readonly IUserService _userService;

    public CalendarController(ICalendarService calendarService, IUserService userService)
    {
        _calendarService = calendarService;
        _userService = userService;
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
        var interviews = await _calendarService.GetInterviewsByCodeAsync(code);

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
    [HttpGet("interviewsByDate")]
    public async Task<IActionResult> GetInterviewsByDate([FromQuery] DateTime selectedDate, [FromQuery] string code)
    {
        var userId = await _userService.GetUserByCodeAsync(code);

        if (userId == null)
        {
            return NotFound("User not found for the provided code.");
        }

        var interviews = await _calendarService.GetInterviewsByDateAsync(userId, selectedDate);

        var simpleInterviews = interviews.Select(i => new SimpleInterview
        {
            Start = i.Start,
            End = i.End,
            Title = i.Title
        });

        return Ok(simpleInterviews);
    }
    public class SimpleInterview
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; }
    }
}
