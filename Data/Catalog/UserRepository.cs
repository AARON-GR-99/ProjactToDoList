using Data.Context;
using Data.Entities;
using Microsoft.Extensions.Logging;

namespace Data.Repisitories.Catalogs;

public class UserRepository(ApplicationDbContext context, ILogger<IUserRepository> logger)
    : BaseRepository<User, IUserRepository>(context, logger), IUserRepository
{
    private readonly ILogger<IUserRepository> _logger1 = logger;

    public async Task<IEnumerable<User>> GetActiveUserAsync()
    {
        _logger1.LogInformation("Fetching active Users");
        return await _dbSet
    }
}