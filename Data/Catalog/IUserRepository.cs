using Data.Entities;

namespace Data.Repisitories.Catalogs;

public interface IUserRepository : IBaseRepository<User>
{
    Task<IEnumerable<User>> GetActiveUsersAsync();
}