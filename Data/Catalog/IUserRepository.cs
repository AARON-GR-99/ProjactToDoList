using Data.Entities;
using Data.Repositories;

namespace Data.Catalog;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByUserNameAsync(string correo);
}