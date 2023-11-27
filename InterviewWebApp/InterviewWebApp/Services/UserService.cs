using Microsoft.EntityFrameworkCore;

public interface IUserService
{
    Task<string> GetUserByCodeAsync(string code);
}
public class UserService : IUserService
{
    private readonly AppDbContext _dbContext;

    public UserService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> GetUserByCodeAsync(string code)
    {
        var shareLink = await _dbContext.ShareLinks
            .Where(sl => sl.Guid.ToString() == code && sl.IsActive)
            .FirstOrDefaultAsync();

        return shareLink?.UserId;
    }
}
