using Data.Catalog;
using Data.Context;
using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repisitories.Catalogs;

public class UserRepository(ApplicationDbContext context)
    : BaseRepository<User>(context), IUserRepository
{
    
    public async Task<User?> GetByUserNameAsync(string correo)
    {
        return await context.Users
            .FirstOrDefaultAsync(u => u.Correo == correo);
    }
}