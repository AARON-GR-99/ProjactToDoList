using Data.Catalog;
using Data.Context;
using Data.Entities;
using Data.Repositories;

namespace Data.Repisitories.Catalogs;

public class UserRepository(ApplicationDbContext context)
    : BaseRepository<User>(context), IUserRepository
{

}