public interface ICalendarService
{
    Task<string> GenerateLinkAsync(string userId);
}
public class CalendarService : ICalendarService
{
    private readonly AppDbContext _dbContext;

    public CalendarService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> GenerateLinkAsync(string userId)
    {
        var guid = Guid.NewGuid();
        var link = $"/calendar/book?code={guid}";

        var shareLink = new ShareLink
        {
            UserId = userId,
            Guid = guid,
            CreatedAt = DateTime.Now,
            IsActive = true
        };

        _dbContext.ShareLinks.Add(shareLink);
        await _dbContext.SaveChangesAsync();

        return link;
    }
}