using Microsoft.EntityFrameworkCore;

public interface ICalendarService
{
    Task<string> GenerateLinkAsync(string userId, string guid);
    Task<List<Interview>> GetInterviewsAsync(string userId);
    Task<List<Interview>> GetInterviewsByCodeAsync(string code);
}
public class CalendarService : ICalendarService
{
    private readonly AppDbContext _dbContext;

    public CalendarService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> GenerateLinkAsync(string userId, string guid)
    {
        var link = $"/calendar/book?code={guid}";

        var shareLink = new ShareLink
        {
            UserId = userId,
            Guid = Guid.Parse(guid),
            CreatedAt = DateTime.Now,
            IsActive = true
        };

        _dbContext.ShareLinks.Add(shareLink);
        await _dbContext.SaveChangesAsync();

        return link;
    }
    public async Task<List<Interview>> GetInterviewsAsync(string userId)
    {
        var interviews = await _dbContext.Interviews
            .Where(i => i.UserId == userId)
            .ToListAsync();

        return interviews;
    }
    public async Task<List<Interview>> GetInterviewsByCodeAsync(string code)
    {
        var shareLink = await _dbContext.ShareLinks
            .Include(sl => sl.User)
            .Where(sl => sl.Guid.ToString() == code && sl.IsActive)
            .FirstOrDefaultAsync();


        if (shareLink == null)
        {
            // Handle case when the link is not found or inactive
            return null;
        }

        return await GetInterviewsAsync(shareLink.UserId);
    }
    
}